using System;
using System.Collections.Generic;
using System.Text;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class GenericComponent
    {
        public GenericComponent(Guid UUID, String IdComponent, Coordinates Coordinates)
        {
            this.UUID = UUID;
            this.IdComponent = IdComponent;
            this.Coordinates = Coordinates;
        }
        public string Id { get; set; }
        public Guid UUID { get; set; }
        public String IdComponent { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
