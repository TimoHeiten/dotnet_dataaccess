using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;
using Microsoft.EntityFrameworkCore;

namespace code.ef
{
    internal class Commands
    {
        public static void Run()
        {
            // SelectAll();
            InnerJoin();
        }

        private static void SelectAll()
        {
           // SELECT * FROM customers
           using (var context = new udemyContext())
           {
               var customers = context.Customers;
               foreach (var item in customers)
               {
                   System.Console.WriteLine(item);
               }
           }
        }

        private static void InnerJoin()
        {
            // SELECT * FROM customers c
            // INNER JOIN orders o
            // on c.Id = o.CustomerId
            // where c.Id < 3;
            using (var context = new udemyContext())
            {
                var customers = context.Customers
                                        .Where(c => c.Id < 3)
                                        .Include(c => c.Orders);
                foreach (var item in customers)
                {
                    System.Console.WriteLine(item);
                    System.Console.WriteLine($"amount of orders: {item.Orders.Count}");
                }
            }
        }
    }
}