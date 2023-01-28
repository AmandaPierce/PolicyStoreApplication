using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Factories
{
    public class MongoDbCollection<T> : IMongoDbCollection<T> where T: IDocument
    {
        public IMongoCollection<T> _collection { get; }

        public MongoDbCollection(MongoClient mongoClient, string databaseName, string collectionName)
        {
            this._collection = mongoClient.GetDatabase(databaseName).GetCollection<T>(collectionName);
        }
    }
}
