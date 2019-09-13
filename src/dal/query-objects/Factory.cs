using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dal.queryobjects
{
    public static class Factory
    {
        private static Dictionary<string, object> _dict = new Dictionary<string, object>
        {
            // ["dal.select"] = new SelectQuery(),
            // ["dal.select"] = new EfQuery(),
            ["dal.select"] = new MongoQuery()
        };

        public static IQuery<T> Create<T>(string queryName)
        {
            return (IQuery<T>)_dict[queryName];
        }
    }
}