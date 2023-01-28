using PolicyStoreApplication.Models.Data;

namespace PolicyStoreApplication.Factories
{
    public interface IMongoDbCollectionFactory
    {
        IMongoDbCollection<T> GetCollection<T>(string collectionName) where T : IDocument;
    }
}
