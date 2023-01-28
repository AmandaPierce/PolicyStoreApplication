using MongoDB.Driver;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Services
{
    public interface IDatabaseService<T>
    {
        Task CreateItem(T item, CancellationToken cancellationToken);
        Task<T> GetItem(string id, CancellationToken cancellationToken);
        Task<T> GetItemByFilter(FilterDefinition<T> filter, CancellationToken cancellationToken);
        Task<List<T>> GetItemsByFilter(FilterDefinition<T> filter, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetItems(CancellationToken cancellationToken);
        Task DeleteItem(string id, CancellationToken cancellationToken);
        Task UpdateItem(string id, T item, CancellationToken cancellationToken);
    }
}
