using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Configuration
{
    public class MongoDBConfiguration
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PolicyCollectionName { get; set; } = null!;
    }
}
