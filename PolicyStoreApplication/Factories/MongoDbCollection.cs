using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;

namespace PolicyStoreApplication.Factories
{
    public class MongoDbCollection<T> : IMongoDbCollection<T> where T: IDocument
    {
        public IMongoCollection<T> _collection { get; }

        /// <summary>
        ///     Gets the specified collection from the database. 
        /// </summary>
        public MongoDbCollection(MongoClient mongoClient, string databaseName, string collectionName)
        {
            this._collection = mongoClient.GetDatabase(databaseName).GetCollection<T>(collectionName);
        }
    }
}
