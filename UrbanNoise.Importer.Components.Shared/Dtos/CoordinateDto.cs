using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CoordinateDto
    {
        [JsonProperty(PropertyName = "latitude")]
        public String Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public String Longitude { get; set; }
    }
}
