using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Implementations;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Infrastructure.Clients;
using UrbanNoise.Importer.Components.Shared.Converters;
using UrbanNoise.Importer.Components.Shared.Dtos;
using UrbanNoise.Importer.Components.Shared.Settings;
using UrbanNoise.Importer.Components.Tests.Unit.Utils.Generators;
using Xunit;

namespace UrbanNoise.Importer.Components.Tests.Unit.Services
{
    [Collection("GenericComponentsImportService Unit Tests")]
    public class GenericComponentServiceUnitTests
    {
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
        }

        [Fact]
        [Trait("Unit Test", "ImportGenericComponents")]
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
        [Trait("Unit Test", "ImportGenericComponents")]
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

        [Theory]
        [Trait("Unit Test", "GetGenericNoiseComponents")]
        [MemberData(nameof(MapComponentsDtoParameter))]
        public void GetGenericNoiseComponents_ShouldReturnNoiseComponents(MapComponentsDto mapComponentsDto)
        {
            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });

            var genericComponentService = new GenericComponentsImportService(_genericComponentRepositoryMocked, optionsMocked.Object, _loggerMocked, _mapComponentsClientMocked);
            
            var result = genericComponentService.GetGenericNoiseComponents(mapComponentsDto);

            Assert.NotNull(result);
            Assert.Collection(result, item => Assert.Contains("1", item.IdComponent));
        }

        public static IEnumerable<object[]> MapComponentsDtoParameter =>
            new[]
            {
                new object[] { GeneratorMapComponentsDto.GenerateMapComponentsDto() }
            };

        [Theory]
        [Trait("Unit Test", "GetGenericNoiseComponents")]
        [MemberData(nameof(WrongMapComponentsDtoParameter))]
        public void GetGenericNoiseComponents_Should_Not_ReturnNoiseComponents(MapComponentsDto mapComponentsDto)
        {
            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });

            var genericComponentService = new GenericComponentsImportService(_genericComponentRepositoryMocked, optionsMocked.Object, _loggerMocked, _mapComponentsClientMocked);

            var result = genericComponentService.GetGenericNoiseComponents(mapComponentsDto);

            Assert.Empty(result);
        }

        public static IEnumerable<object[]> WrongMapComponentsDtoParameter =>
            new[]
            {
                new object[] { GeneratorMapComponentsDto.GenerateWrongMapComponentsDto() }
            };

        [Theory]
        [Trait("Unit Test", "GenericComponentsHaveChanged")]
        [MemberData(nameof(GenericComponentsParameter))]
        public async Task GenericComponentsHaveChanged_Should_Not_ReturnComponentsToInsertOrToDelete(IEnumerable<GenericComponent> genericComponents)
        {
            var genericComponentRepositoryMocked = new Mock<IGenericComponentRepository>();

            genericComponentRepositoryMocked.Setup(x => x.GetGenericComponents()).ReturnsAsync(await GeneratorGenericComponents.GenerateGenericComponentsAsync());

            var genericComponentService = new GenericComponentsImportService(genericComponentRepositoryMocked.Object, _optionsMocked, _loggerMocked, _mapComponentsClientMocked);

            var (componentsToInsert, componentsToDelete) = await genericComponentService.GenericComponentsHaveChanged(genericComponents);

            Assert.Empty(componentsToInsert);
            Assert.Empty(componentsToDelete);
        }

        [Theory]
        [Trait("Unit Test", "GenericComponentsHaveChanged")]
        [MemberData(nameof(GenericComponentsParameter))]
        public async Task GenericComponentsHaveChanged_ShouldReturnComponentsToInsert(IEnumerable<GenericComponent> genericComponents)
        {
            var genericComponentRepositoryMocked = new Mock<IGenericComponentRepository>();

            genericComponentRepositoryMocked.Setup(x => x.GetGenericComponents())
                .ReturnsAsync(new List<GenericComponent> { GeneratorGenericComponents.GenerateGenericComponentsAsync().Result.FirstOrDefault() });

            var genericComponentService = new GenericComponentsImportService(genericComponentRepositoryMocked.Object, _optionsMocked, _loggerMocked, _mapComponentsClientMocked);

            var (componentsToInsert, componentsToDelete) = await genericComponentService.GenericComponentsHaveChanged(genericComponents);

            Assert.NotEmpty(componentsToInsert);
            Assert.Empty(componentsToDelete);
        }

        [Theory]
        [Trait("Unit Test", "GenericComponentsHaveChanged")]
        [MemberData(nameof(GenericComponentsParameter))]
        public async Task GenericComponentsHaveChanged_ShouldReturnComponentsToDelete(IEnumerable<GenericComponent> genericComponents)
        {
            var genericComponentRepositoryMocked = new Mock<IGenericComponentRepository>();

            genericComponentRepositoryMocked.Setup(x => x.GetGenericComponents())
                .ReturnsAsync(GeneratorGenericComponents.GenerateGenericComponentsAsync().Result.Append(GeneratorGenericComponents.GenerateGenerateComponent()));

            var genericComponentService = new GenericComponentsImportService(genericComponentRepositoryMocked.Object, _optionsMocked, _loggerMocked, _mapComponentsClientMocked);

            var (componentsToInsert, componentsToDelete) = await genericComponentService.GenericComponentsHaveChanged(genericComponents);

            Assert.Empty(componentsToInsert);
            Assert.NotEmpty(componentsToDelete);
        }

        public static IEnumerable<object[]> GenericComponentsParameter =>
            new[]
            {
                new object[] { GeneratorGenericComponents.GenerateGenericComponents() }
            };

        [Fact]
        [Trait("Unit Test", "SaveGenericNoiseComponents")]
        public async Task SaveGenericNoiseComponents_ShouldInsertNewComponents()
        {
            var mapComponentsDto = GeneratorMapComponentsDto.GenerateMapComponentsDtoAsync().Result;
            mapComponentsDto.Components.Add(GeneratorMapComponentsDto.GenerateMapComponentDto());
            
            var mapComponentsClientMocked = new Mock<IMapComponentsClient>();
            mapComponentsClientMocked.Setup(x => x.GetMapComponentsFromBcnConnectaApi())
                .ReturnsAsync(mapComponentsDto);

            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });

            var genericComponentRepositoryMocked = new Mock<IGenericComponentRepository>();
            genericComponentRepositoryMocked.Setup(x => x.GetGenericComponents())
                .ReturnsAsync(new List<GenericComponent> { GeneratorGenericComponents.GenerateGenericComponentsAsync().Result.FirstOrDefault() });

            var genericComponentService = new GenericComponentsImportService(genericComponentRepositoryMocked.Object, optionsMocked.Object, _loggerMocked, mapComponentsClientMocked.Object);

            var (newComponentsInserted, unusedComponentsDeleted) = await genericComponentService.SaveGenericNoiseComponents();

            Assert.True(newComponentsInserted);
            Assert.False(unusedComponentsDeleted);
        }

        [Fact]
        [Trait("Unit Test", "SaveGenericNoiseComponents")]
        public async Task SaveGenericNoiseComponents_ShouldDeleteUnusedComponents()
        {
            var mapComponentsDto = GeneratorMapComponentsDto.GenerateMapComponentsDtoAsync().Result;
            mapComponentsDto.Components.RemoveAt(1);

            var mapComponentsClientMocked = new Mock<IMapComponentsClient>();
            mapComponentsClientMocked.Setup(x => x.GetMapComponentsFromBcnConnectaApi())
                .ReturnsAsync(mapComponentsDto);

            var optionsMocked = new Mock<IOptions<AppSettings>>();
            optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { SensorType = "noise" } });

            var genericComponentRepositoryMocked = new Mock<IGenericComponentRepository>();
            genericComponentRepositoryMocked.Setup(x => x.GetGenericComponents())
                .ReturnsAsync(await GeneratorGenericComponents.GenerateGenericComponentsAsync());

            var genericComponentService = new GenericComponentsImportService(genericComponentRepositoryMocked.Object, optionsMocked.Object, _loggerMocked, mapComponentsClientMocked.Object);

            var (newComponentsInserted, unusedComponentsDeleted) = await genericComponentService.SaveGenericNoiseComponents();

            Assert.False(newComponentsInserted);
            Assert.True(unusedComponentsDeleted);
        }
    }
}
