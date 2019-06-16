using System.Collections.Generic;

namespace nosql
{
    internal class Customer
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
    }
}