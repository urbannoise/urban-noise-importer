using Newtonsoft.Json;
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapComponentsDto
    {
        [DeserializeAs(Name = "components")]
        public IList<MapComponentDto> Components { get; set; }
    }
}
