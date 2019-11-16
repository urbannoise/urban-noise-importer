using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class NoiseComponent
    {
        public string Id { get; set; }
        public String IdComponent { get; set; }
        public String Component { get; set; }
        public Guid UUID { get; set; }
        public NoiseSensor NoiseSensor { get; set; }
    }
}
