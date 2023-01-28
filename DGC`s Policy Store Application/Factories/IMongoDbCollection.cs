using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Factories
{
    public interface IMongoDbCollection<T> where T: IDocument
    {
        IMongoCollection<T> _collection { get; }
    }
}
