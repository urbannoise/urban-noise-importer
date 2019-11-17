using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapComponentDto
    {
        [JsonProperty(PropertyName = "icon")]
        public String Icon { get; set; }

        [JsonProperty(PropertyName = "id")]
        public String IdComponent { get; set; }

        [JsonProperty(PropertyName = "type")]
        public String ComponentType { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public IList<CoordinateDto> Coordinates { get; set; }

        [JsonProperty(PropertyName = "centroid")]
        public CoordinateDto CentroId { get; set; }
    }
}
