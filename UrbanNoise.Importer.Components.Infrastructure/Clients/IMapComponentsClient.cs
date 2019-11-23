using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Shared.Dtos;

namespace UrbanNoise.Importer.Components.Infrastructure.Clients
{
    public interface IMapComponentsClient
    {
        Task<MapComponentsDto> GetMapComponentsFromBcnConnectaApi();
    }
}
