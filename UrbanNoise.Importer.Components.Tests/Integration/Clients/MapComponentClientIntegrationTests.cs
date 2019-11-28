using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Infrastructure.Clients;
using UrbanNoise.Importer.Components.Shared.Settings;
using Xunit;

namespace UrbanNoise.Importer.Components.Tests.Integration.Clients
{
    public class MapComponentClientIntegrationTests
    {
        private readonly Mock<IOptions<AppSettings>> _optionsMocked;

        public MapComponentClientIntegrationTests()
        {
            _optionsMocked = new Mock<IOptions<AppSettings>>();
            _optionsMocked.Setup(i => i.Value).Returns(new AppSettings { BcnConnectaApi = new BcnConnectaApi { BaseUri = "http://connecta.bcn.cat/connecta-catalog-web/component/map/json", SensorType = "noise" } });
        }

        [Fact]
        public async Task GetMapComponentsFromBcnConnectaApi_ShouldGetDataFromApi()
        {
            var mapComponentsClient = new MapComponentsClient(_optionsMocked.Object);

            var result = await mapComponentsClient.GetMapComponentsFromBcnConnectaApi();

            Assert.NotNull(result);
        }
    }
}
