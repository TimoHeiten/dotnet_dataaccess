using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code.ado.shared
{
    internal static class Constants
    {
        internal static string SQLITE_CONNECTION = $"DataSource={Environment.CurrentDirectory}/data/sqlite/ecommerce.db;Version=3;";
        internal static string PSQL_CONNECTION = "Server=localhost;Database=udemy;User Id=udemy;Port=5433";
    }
}