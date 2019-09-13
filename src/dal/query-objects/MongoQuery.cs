using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using dal.queryobjects;
using code.ef.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace dal.queryobjects
{
    public class MongoQuery : IQuery<IEnumerable<Customers>>
    {
        public IEnumerable<Customers> Execute()
        {
            var collection = Connect();
            var list = new List<Customers>();
            using (var cursor = collection.FindSync(new BsonDocument()))
            {
                while (cursor.MoveNext())
                {
                    var batch = cursor.Current;
                    list.AddRange(batch.Select(x => FromBson(x)));
                }
            }
            return list;
        }

        private Customers FromBson(BsonDocument bson)
        {
            if (bson.Contains("name"))
            {
                return new Customers
                {
                    Name = bson["name"].ToString(),
                    Lastname = bson["lastname"].ToString(),
                };
            }
            else
            {
                return new Customers
                {
                    Name = bson["Name"].ToString(),
                    Lastname = bson["LastName"].ToString()
                };
            }
        }

        private IMongoCollection<BsonDocument> Connect()
        {
            string connection = "mongodb://localhost:5438";
            var client = new MongoClient(connection);
            IMongoDatabase db = client.GetDatabase("udemy"); // creates it
            var collection = db.GetCollection<BsonDocument>("customers");
            if (collection == null)
            {
                db.CreateCollection("customers");
            }

            return collection ?? db.GetCollection<BsonDocument>("customers");
        }

    }
}