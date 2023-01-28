using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Factories
{
    public interface IMongoDbCollectionFactory
    {
        IMongoDbCollection<T> GetCollection<T>(string collectionName) where T : IDocument;
    }
}
