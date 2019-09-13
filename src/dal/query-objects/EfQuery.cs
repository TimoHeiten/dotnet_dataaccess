using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using code.ef.Models;

namespace dal.queryobjects
{
    public class EfQuery : IQuery<IEnumerable<Customers>>
    {
        public IEnumerable<Customers> Execute()
        {
            using (var context = new udemyContext())
            {
                return context.Customers.ToList();
            }
        }
    }
}