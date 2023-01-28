using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Models.Data
{
    public class IDocument
    {
        [BsonId]
        public string Id { get; set; }
    }
}
