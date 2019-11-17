using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Shared.Converters;
using UrbanNoise.Importer.Components.Shared.Dtos;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Business.Implementations
{
    public class GenericComponentsImportService : IGenericComponentsImportService
    {
        private readonly IGenericComponentRepository _genericComponentRepository;
        private readonly AppSettings _appSettings;
        private readonly IRestClient _client;
        private readonly ILogger _logger;

        public GenericComponentsImportService(IGenericComponentRepository genericComponentRepository, IOptions<AppSettings> options, ILogger logger)
        {
            _genericComponentRepository = genericComponentRepository;
            _appSettings = options.Value;
            _logger = logger;

            _client = new RestClient(_appSettings.BcnConnectaApi.BaseUri);
        }


        public async Task<IEnumerable<GenericComponent>> ImportGenericComponents()
        {
            IEnumerable<GenericComponent> noiseComponents = new List<GenericComponent>();

            try
            {
                var request = new RestRequest();

                var allComponents = await Execute<MapComponentsDto>(request);

                //Calling a method to get only the noise sensors
                noiseComponents = GetNoiseSensors(allComponents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error importing the components from Bcn Conecta API. Exception: {ex.Message}");
            }
            return noiseComponents;
        }

        public async Task SaveNoiseComponents()
        {
            var noiseComponents = await ImportGenericComponents();
            await _genericComponentRepository.SaveGenericComponents(noiseComponents);
        }

        public async Task<T> Execute<T>(RestRequest request) where T : new ()
        {
            request.RequestFormat = DataFormat.Json;
            var response = await _client.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                _logger.LogError("Error retrieving response.  Check inner details for more info.");             
                throw new Exception("Exception: ", response.ErrorException); 
            }
            return response.Data;
        }

        public Boolean GenericComponentsHaveChanged(IEnumerable<GenericComponent> genericComponents)
        {
            //We need to verify if there is any change on the list of Noise Sensors
            return genericComponents.Equals(_genericComponentRepository.GetGenericComponents());
        }

        public IEnumerable<GenericComponent> GetNoiseSensors(MapComponentsDto mapComponentsDto)
        {
            return GenericComponentConverter.MapToEntity(mapComponentsDto.Components.Where(i => i.Icon.Equals(_appSettings.BcnConnectaApi.SensorType)).ToList());
        }
    }
}
