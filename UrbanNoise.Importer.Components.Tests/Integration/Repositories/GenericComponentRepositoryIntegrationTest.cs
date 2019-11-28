using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Infrastructure.Implementations;
using UrbanNoise.Importer.Components.Shared.Settings;
using UrbanNoise.Importer.Components.Tests.Unit.Utils.Generators;
using Xunit;

namespace UrbanNoise.Importer.Components.Tests.Integration.Repositories
{
    [Collection("GenericComponentRepositoryIntegrationTest Integration Tests")]
    public class GenericComponentRepositoryIntegrationTest
    {

        private readonly Mock<IOptions<AppSettings>> _optionsMocked;
        private readonly IEnumerable<GenericComponent> _genericComponents;
        private readonly IGenericComponentRepository _genericComponentRepository;

        public GenericComponentRepositoryIntegrationTest()
        {
            MongoIntegrationTest.CreateConnection();

            _optionsMocked = new Mock<IOptions<AppSettings>>();
            _optionsMocked.Setup(i => i.Value).Returns(new AppSettings { CosmosDb = new CosmosDb { ConnectionString = MongoIntegrationTest._runner.ConnectionString, Collection = "TestCollection", Database = "IntegrationTest" } });
            _genericComponents = GeneratorGenericComponents.GenerateGenericComponents();

            _genericComponentRepository = new GenericComponentRepository(_optionsMocked.Object);
        }

        [Fact]
        [Trait("Integration Test", "SaveGenericComponents")]
        public async Task SaveGenericComponents_ShouldSaveComponents()
        {    
            var result = await _genericComponentRepository.SaveGenericComponents(_genericComponents);

            Assert.NotNull(result);
            Assert.Equal(_genericComponents, result);
        }


        [Fact]
        [Trait("Integration Test", "GetGenericComponents")]
        public async Task GetGenericComponents_ShouldGetComponents()
        {
            var saveResult = await _genericComponentRepository.SaveGenericComponents(_genericComponents);
            var getResult = await _genericComponentRepository.GetGenericComponents();

            Assert.NotNull(getResult);
            Assert.IsAssignableFrom<IEnumerable<GenericComponent>>(getResult);
            Assert.Equal(saveResult.Count(), getResult.Count());
        }

        [Fact]
        [Trait("Integration Test", "DeleteGenericComponents")]
        public async Task DeleteGenericComponents_ShouldDeleteComponents()
        {
            var saveResult = await _genericComponentRepository.SaveGenericComponents(_genericComponents);
            var result = await _genericComponentRepository.DeleteGenericComponents(_genericComponents);

            Assert.True(result > 0);
            Assert.Equal(saveResult.Count(), result);
        }
    }
}
