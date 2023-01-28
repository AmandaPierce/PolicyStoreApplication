using MongoDB.Driver;
using PolicyStoreApplication.Factories;
using PolicyStoreApplication.Models.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Services
{
    public class DatabaseService<T> : IDatabaseService<T> where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;

        public DatabaseService(IMongoDbCollectionFactory mongoDbCollectionFactory, string collectionName)
        {
            _collection = mongoDbCollectionFactory.GetCollection<T>(collectionName)._collection;
        }

        public async Task CreateItem(T item, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<T> GetItem(string id, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> GetItemByFilter(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetItemsByFilter(FilterDefinition<T> filter, CancellationToken cancellationToken)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetItems(CancellationToken cancellationToken)
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task DeleteItem(string id, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
        public async Task UpdateItem(string id, T item, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, item);
        }

    }
}
