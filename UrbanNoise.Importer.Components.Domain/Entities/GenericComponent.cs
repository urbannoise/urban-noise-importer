using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class GenericComponent
    {
        public GenericComponent(ObjectId ObjectId, String IdComponent, Coordinates Coordinates)
        {
            this.ObjectId = ObjectId;
            this.IdComponent = IdComponent;
            this.Coordinates = Coordinates;
        }
        public string Id { get; set; }
        
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public String IdComponent { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
