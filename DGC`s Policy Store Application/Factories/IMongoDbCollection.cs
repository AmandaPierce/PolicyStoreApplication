using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;

namespace PolicyStoreApplication.Factories
{
    public interface IMongoDbCollection<T> where T: IDocument
    {
        IMongoCollection<T> _collection { get; }
    }
}
