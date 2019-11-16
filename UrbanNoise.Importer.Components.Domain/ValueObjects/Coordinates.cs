using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Domain.ValueObjects
{
    public class Coordinates
    {
        public Coordinates(String Latitude, String Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public String Latitude { get; set; }
        public String Longitude { get; set; }
    }
}
