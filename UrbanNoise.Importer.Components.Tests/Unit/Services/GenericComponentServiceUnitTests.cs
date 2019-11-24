using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Implementations;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Infrastructure.Clients;
using UrbanNoise.Importer.Components.Shared.Converters;
using UrbanNoise.Importer.Components.Shared.Settings;
using UrbanNoise.Importer.Components.Tests.Unit.Utils.Generators;
using Xunit;

namespace UrbanNoise.Importer.Components.Tests.Unit.Services
{
    [Collection("GenericComponentsImportService Unit Tests")]
    public class GenericComponentServiceUnitTests
    {
        private readonly IGenericComponentsImportService _genericComponentService;
        private readonly IGenericComponentRepository _genericComponentRepositoryMocked;
        private readonly IOptions<AppSettings> _optionsMocked;
        private readonly ILogger _loggerMocked;
        private readonly IMapComponentsClient _mapComponentsClientMocked;

        public GenericComponentServiceUnitTests()
        {
            _optionsMocked = Options.Create<AppSettings>(new AppSettings());
            _loggerMocked = Mock.Of<ILogger>();
            _mapComponentsClientMocked = Mock.Of<IMapComponentsClient>();

            _genericComponentRepositoryMocked = Mock.Of<IGenericComponentRepository>();

            _genericComponentService = new GenericComponentsImportService(_genericComponentRepositoryMocked, _optionsMocked, _loggerMocked, _mapComponentsClientMocked);
        }

        [Fact]
        [Trait("Category", "ImportGenericComponents")]
        public async Task ImportGenericComponents_ShouldReturnGenericComponents()
        {
            var mapComponentsClientMocked = new Mock<IMapComponentsClient>();
            
            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });
            
            var genericComponentService = new GenericComponentsImportService(_genericComponentRepositoryMocked, optionsMocked.Object, _loggerMocked, mapComponentsClientMocked.Object);

            mapComponentsClientMocked.Setup(x => x.GetMapComponentsFromBcnConnectaApi())
                .ReturnsAsync(await GeneratorMapComponentsDto.GenerateMapComponentsDtoAsync());

            var result = await genericComponentService.ImportGenericComponents();
            var expected = GenericComponentConverter.MapToEntity(GeneratorMapComponentsDto.GenerateMapComponentsDtoAsync().Result.Components.Where(i => i.Icon.Equals("noise")));
            
            Assert.Equal(expected.SingleOrDefault().IdComponent, result.SingleOrDefault().IdComponent);
        }

        [Fact]
        [Trait("Category", "ImportGenericComponents")]
        public async Task ImportGenericComponents_Should_Not_ReturnGenericComponents()
        {
            var mapComponentsClientMocked = new Mock<IMapComponentsClient>();

            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });

            var genericComponentService = new GenericComponentsImportService(_genericComponentRepositoryMocked, optionsMocked.Object, _loggerMocked, mapComponentsClientMocked.Object);

            mapComponentsClientMocked.Setup(x => x.GetMapComponentsFromBcnConnectaApi())
                .ReturnsAsync(await GeneratorMapComponentsDto.GenerateWrongMapComponentsDtoAsync());

            var result = await genericComponentService.ImportGenericComponents();
            
            Assert.Empty(result);
        }
    }
}
