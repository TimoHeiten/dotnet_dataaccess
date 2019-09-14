using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;
using code.ado.shared;
using Dapper;

namespace dal.repositorypattern
{
    public class DapperRepository : IRepository<Customers>, IUnitOfWork
    {
        private Npgsql.NpgsqlConnection Connect => new Npgsql.NpgsqlConnection(Constants.PSQL_CONNECTION);
        private readonly List<Task> _dalTask = new List<Task>();
        public Task AddAsync(Customers customer)
        {
            // another way is using transactions
            string insert = "INSERT INTO customers VALUES (DEFAULT, @Name, @LastName);";
            _dalTask.Add(new Task(async () => 
                {
                    using (var connection = Connect)
                    {
                        await connection.OpenAsync();
                        await connection.ExecuteAsync(insert,
                            new { Name = customer.Name, LastName = customer.Lastname });
                    }
                }
            ));
            return Task.CompletedTask;
        }

        public async Task CommitAsync()
        {
            foreach (var task in _dalTask)
            {
                task.Start();
            }
            await Task.WhenAll(_dalTask);

            _dalTask.Clear();
            // error handling etc.
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            string sql = "SELECT * FROM customers;";
            using (var connect = Connect)
            {
                await connect.OpenAsync();
                var customers = await connect.QueryAsync<Customers>(sql);
                return customers.ToList();
            }
        }
    }
}