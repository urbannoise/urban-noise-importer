using Newtonsoft.Json;
using RestSharp.Deserializers;
using System;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CoordinateDto
    {
        [DeserializeAs(Name = "latitude")]
        public String Latitude { get; set; }

        [DeserializeAs(Name = "longitude")]
        public String Longitude { get; set; }
    }
}
