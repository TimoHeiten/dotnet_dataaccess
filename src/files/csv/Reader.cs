using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace code.files.csv
{
    internal class Reader
    {
        public static void Run()
        {
            // load both
            // merge or rather: Join
            string files = $"{Environment.CurrentDirectory}/data/files/";
            string[] orderLines = Parse(files + "orders.csv");
            string[] customerLines= Parse(files + "customers.csv");

            var customers = LoadCustomers(customerLines);
            var orders = LoadOrders(orderLines);

            var finalResult = Merge(customers, orders);

            finalResult.ToList().ForEach(x => System.Console.WriteLine(x));

        }

        private static string[] Parse(string fileName)
        {
            // first is the header 
            return File.ReadAllLines(fileName).Skip(1).ToArray();
        }

        private static IEnumerable<Customer> LoadCustomers(string[] array)
        {
            return array.Select(line => 
            {
                string[] entity = line.Split(";");
                return new Customer
                {
                    Id = entity[0],
                    Name = entity[1],
                    LastName = entity[2]
                };
            });
        }

        private static IEnumerable<Order> LoadOrders(string[] array)
        {
            return array.Select(line => 
            {
                string[] entity = line.Split(";");
                return new Order
                {
                    Id = entity[0],
                    Name = entity[1],
                    CustomerId = entity[2]
                };
            });
        }

        private static IEnumerable<Customer> Merge(IEnumerable<Customer> customers,
            IEnumerable<Order> orders)
        {
            foreach (var item in customers)
            {
                foreach (var order in orders)
                {
                    if (order.CustomerId == item.Id)
                        item.Orders.Add(order);
                }
            }
            return customers;
        }

        private class Customer
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }

            public List<Order> Orders { get; set; } = new List<Order>();

            public override string ToString() 
            {
                string nl = Environment.NewLine;
                return $"Customer:{nl}Id:{Id}{nl}Name:{Name}, Lastname:{LastName}{nl}"
                    + $"order amount: {Orders.Count}";
            }
        }
        private class Order
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string CustomerId { get; set; }
        }
    }
}