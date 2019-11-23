using Microsoft.Extensions.Options;
using RestSharp;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Shared.Dtos;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Infrastructure.Clients
{
    public class MapComponentsClient : BaseClient, IMapComponentsClient
    {
        private readonly AppSettings _appSettings;

        public MapComponentsClient(IOptions<AppSettings> options) : base ()
        {
            _appSettings = options.Value ;
        }

        public async Task<MapComponentsDto> GetMapComponentsFromBcnConnectaApi()
        {
            RestRequest request = new RestRequest(_appSettings.BcnConnectaApi.BaseUri);
            return await GetAsync<MapComponentsDto>(request);
        }
    }
}
