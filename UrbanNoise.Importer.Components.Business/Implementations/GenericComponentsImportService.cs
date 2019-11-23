using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Domain.Comparers;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Infrastructure.Clients;
using UrbanNoise.Importer.Components.Shared.Converters;
using UrbanNoise.Importer.Components.Shared.Dtos;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Business.Implementations
{
    public class GenericComponentsImportService : IGenericComponentsImportService
    {
        private readonly IGenericComponentRepository _genericComponentRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapComponentsClient _mapComponentsClient;
        private readonly ILogger _logger;

        public GenericComponentsImportService(IGenericComponentRepository genericComponentRepository, IOptions<AppSettings> options, ILogger logger, IMapComponentsClient mapComponentsClient)
        {
            _appSettings = options.Value;
            _logger = logger;

            _genericComponentRepository = genericComponentRepository;
            _mapComponentsClient = mapComponentsClient;
        }


        public async Task<IEnumerable<GenericComponent>> ImportGenericComponents()
        {
            IEnumerable<GenericComponent> genericNoiseComponents = new List<GenericComponent>();

            try
            {
                //ar request = new RestSharp.RestRequest();

                var allGenericComponents = await _mapComponentsClient.GetMapComponentsFromBcnConnectaApi();

                //Calling a method to get only the noise sensors
                genericNoiseComponents = GetGenericNoiseComponents(allGenericComponents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error importing the components from Bcn Conecta API. Exception: {ex.Message}");
            }
            return genericNoiseComponents;
        }

        public async Task SaveGenericNoiseComponents()
        {
            var noiseComponents = await ImportGenericComponents();

            if (await GenericComponentsHaveChanged(noiseComponents))
            {
                await _genericComponentRepository.SaveGenericComponents(noiseComponents);
            }
        }

        public async Task<Boolean> GenericComponentsHaveChanged(IEnumerable<GenericComponent> genericComponents)
        {
            //We need to verify if there is any change on the list of Noise Sensors
            var newComponents = false;

            newComponents = !genericComponents.SequenceEqual(await _genericComponentRepository.GetGenericComponents(), new GenericComponentComparer());

            return newComponents;
        }

        public IEnumerable<GenericComponent> GetGenericNoiseComponents(MapComponentsDto mapComponentsDto)
        {
            return GenericComponentConverter.MapToEntity(mapComponentsDto.Components.Where(i => i.Icon.Equals(_appSettings.BcnConnectaApi.SensorType)).ToList());
        }
    }
}
