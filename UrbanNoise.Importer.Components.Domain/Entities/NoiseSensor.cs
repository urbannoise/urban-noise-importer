using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Domain.Entities
{
    public class NoiseSensor
    {
        public String Id { get; set; }
        public Boolean IsOnline { get; set; }
        public Double Value { get; set; }
        public String Unit { get; set; }
        public DateTime DateLastUpdate { get; set; }
        public Boolean Found { get; set; }
    }
}
