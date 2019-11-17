using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapComponentsDto
    {
        [JsonProperty(PropertyName = "components")]
        public IList<MapComponentDto> Components { get; set; }
    }
}
