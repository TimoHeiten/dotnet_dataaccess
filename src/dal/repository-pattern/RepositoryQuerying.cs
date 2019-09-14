using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;

namespace dal.repositorypattern
{
    public class RepositoryQuerying
    {
        public static void Run()
        {
           var dapper = new DapperRepository();
           var ef = new EfRepository(new udemyContext());

        //    EfThenDapper(dapper, ef);
           DapperThenEf(dapper, ef);

        }

        private static void EfThenDapper(DapperRepository dapper, EfRepository ef)
        {
            var addedCustomer = new Customers
            {
                Name = "Ef-Customer",
                Lastname = "FromRepository"
            };
            ef.AddAsync(addedCustomer).Wait();
            System.Console.WriteLine("added but not committed yet!");
            QueryAllWith(dapper);
            Commit(ef);
            System.Console.WriteLine("after commit! :");
            QueryAllWith(dapper);
        }

        private static void DapperThenEf(DapperRepository dapper, EfRepository ef)
        {
           var addedCustomer = new Customers
            {
                Name = "Dapper-Customer",
                Lastname = "FromRepository"
            };
            dapper.AddAsync(addedCustomer).Wait();
            System.Console.WriteLine("added but not committed yet!");
            QueryAllWith(ef);
            Commit(dapper);
            System.Console.WriteLine("after commit! :");
            QueryAllWith(ef);
        }

        private static void Commit(IUnitOfWork unit) => unit.CommitAsync().Wait();

        private static void AddCustomer(IRepository<Customers> repository, Customers addedCustomer)
        {
            repository.AddAsync(addedCustomer).Wait();
        }

        private static void QueryAllWith(IRepository<Customers> repository)
        {
            System.Console.WriteLine("");
            string result = string.Join("\n", repository.GetAllAsync().Result);
            System.Console.WriteLine($"queried from: {repository.GetType().Name}\n{result}");
        }

    }
}