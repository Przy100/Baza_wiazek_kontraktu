using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Baza_wiazek_przyciskow_20240205.Server
{
    // Struktura jak na serwerze.
    public class UpdateDB
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("date")]
        public int Date { get; set; }

        [BsonElement("author")]
        public int Author { get; set; }

    }
}
