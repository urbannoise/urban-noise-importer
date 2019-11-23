using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace UrbanNoise.Importer.Components.Shared.Dtos
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MapComponentDto
    {
        [DeserializeAs(Name = "icon")]
        public string Icon { get; set; }

        [DeserializeAs(Name = "id")]
        public string IdComponent { get; set; }

        [DeserializeAs(Name = "type")]
        public string ComponentType { get; set; }

        [DeserializeAs(Name = "centroid")]
        public CoordinateDto Coordinates { get; set; }
    }
}
