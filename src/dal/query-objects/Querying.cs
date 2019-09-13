using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;

namespace dal.queryobjects
{
    public class Querying
    {
        public static void Run()
        {
            var query = Factory.Create<IEnumerable<Customers>>("dal.select");

            var customers = query.Execute();

            System.Console.WriteLine(string.Join("\n", customers));
        }
    }
}