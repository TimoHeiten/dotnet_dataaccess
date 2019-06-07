using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using code.ado.shared;

namespace code.ado.transaction
{
    internal class Acid
    {
        private const string INSERT_FAIL = 
            "INSERT INTO Orders (name, customerId) VALUES ('ordered', 111)";
        public static void Run()
        {
            Exceptional();
        }

        public static void Exceptional()
        {
            Insert(Constants.GetSqliteConnection());
            Insert(Constants.GetPsqlConnection(), "psql");
        }

        private static void Insert(DbConnection connection, string provider = "sqlite")
        {
            try
            {
                System.Console.WriteLine($"provider: {provider} ");
                using (connection)
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = INSERT_FAIL;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.GetType().Name);
            }
        }



        public static void Transactional(DbConnection connection, string provider = "sqlite")
        {
            string UPDATE = 
                "UPDATE Customers SET Id=42 WHERE id=1";
            string INSERT = 
                "INSERT INTO Orders (name, CustomerId) VALUES ('ordered', 1)";
            
            System.Console.WriteLine($"provider: {provider} ");

            DbTransaction transaction = null;
            try
            {
                using (connection)
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = UPDATE;
                        int affected = cmd.ExecuteNonQuery();
                        System.Console.WriteLine($"update affected: {affected}");
                    }
                    using (var cmd2 = connection.CreateCommand())
                    {
                        cmd2.CommandText = INSERT;
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    System.Console.WriteLine(ex.Message);
                }
                catch (System.Exception ex2)
                {
                    System.Console.WriteLine(ex2.Message);
                }
            }
           

        }
    }
}