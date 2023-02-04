using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace FreeCourse.Service.Catalog.Models
{
    internal class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }

        public string UserID { get; set; }

        public Feature Feature { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryID { get; set; }

        [BsonIgnore] //Database'de oluşturma
        public Category Category { get; set; }
    }
}
