using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baza_wiazek_przyciskow_20240205.Server
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("user")]
        public string User { get; set; }

        [BsonElement("version")]
        public int Version { get; set; }

    }
}
