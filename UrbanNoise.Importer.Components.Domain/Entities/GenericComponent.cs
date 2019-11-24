using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class GenericComponent
    {
        public GenericComponent()
        {

        }
        public GenericComponent(ObjectId ObjectId, string IdComponent, Coordinates Coordinates)
        {
            this.Id = ObjectId;
            this.IdComponent = IdComponent;
            this.Coordinates = Coordinates;
        }
        
        [BsonId]
        public ObjectId Id { get; set; }
        public string IdComponent { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
