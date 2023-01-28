using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolicyStoreApplication.Factories
{
    public class MongoDbCollectionFactory : IMongoDbCollectionFactory
    {
        private readonly MongoClient _mongoClient;
        private readonly string _databaseName;
        private readonly List<string> _collectionNames;

        public MongoDbCollectionFactory(MongoClient mongoClient, string databaseName, List<string> collectionNames)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionNames = collectionNames ?? throw new ArgumentNullException(nameof(collectionNames));
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        /// <summary>
        ///     Retrieves a mongo db collection from the database. 
        /// </summary>
        public IMongoDbCollection<T> GetCollection<T>(string collectionName) where T : IDocument
        {
            if(_collectionNames.Where(n => n == collectionName) == null)
            {
                throw new ArgumentException($"Unable to find collection: {collectionName}");
            }

            return new MongoDbCollection<T>(_mongoClient, _databaseName, collectionName);
        }

    }
}
