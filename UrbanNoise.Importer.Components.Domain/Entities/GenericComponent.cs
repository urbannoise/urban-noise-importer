using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class GenericComponent
    {
        public GenericComponent(ObjectId ObjectId, String IdComponent, Coordinates Coordinates)
        {
            this.Id = ObjectId;
            this.IdComponent = IdComponent;
            this.Coordinates = Coordinates;
        }
        
        [BsonId]
        public ObjectId Id { get; set; }
        public String IdComponent { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
