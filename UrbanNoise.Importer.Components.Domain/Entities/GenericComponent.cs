using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class GenericComponent : IEquatable<GenericComponent>
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

        public override bool Equals(object obj) => Equals(obj as GenericComponent);

        public bool Equals(GenericComponent genericComponentSecond)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(this, genericComponentSecond)) return true;

            //Check the properties
            return this.IdComponent == genericComponentSecond.IdComponent
                && this.Coordinates.Latitude == genericComponentSecond.Coordinates.Latitude
                && this.Coordinates.Longitude == genericComponentSecond.Coordinates.Longitude;
        }
    }
}
