using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;
using System.Data;
using code.ado.shared;
using Dapper;

namespace dal.queryobjects
{
    internal class SelectQuery : IQuery<IEnumerable<Customers>>
    {
        private IDbConnection Connect => new Npgsql.NpgsqlConnection(Constants.PSQL_CONNECTION);
        public IEnumerable<Customers> Execute()
        {
            using (var connection = Connect)
            {
                return connection.Query<Customers>("SELECT * FROM customers;").ToList();
            }
        }
    }
}