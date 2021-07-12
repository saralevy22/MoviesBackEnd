using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movies.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MovieId { get; set; }
        public string MovieName { get; set; }
        public string Category { get; set; }
        public string ImdbLink { get; set; }
        public string ImdbPosterLink { get; set; }
        public DateTime? createDate { get; set; }
        
    }
}
