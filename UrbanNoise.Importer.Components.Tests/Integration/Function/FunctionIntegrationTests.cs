using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Implementations;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Tests.Integration.Utils;
using Xunit;

namespace UrbanNoise.Importer.Components.Tests.Integration.Function
{
    public class FunctionIntegrationTests
    {
        [Fact]
        public async Task Timer_should_log_message()
        {
            var genericComponentsImportService = new Mock<IGenericComponentsImportService>();
            genericComponentsImportService.Setup(x => x.SaveGenericNoiseComponents()).Returns(Task.FromResult((true, true)));

            Application.Function function = new Application.Function(genericComponentsImportService.Object);
            var loggerMocked = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            await function.Run(null, loggerMocked);
            var msg = loggerMocked.Logs[0];
            Assert.Contains("C# Timer trigger function executed at", msg);
        }
    }
}
