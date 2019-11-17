using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Application
{
    public class Function
    {
        private readonly IGenericComponentsImportService _genericComponentsImportService;

        public Function(IGenericComponentsImportService genericComponentsImportService)
        {
            _genericComponentsImportService = genericComponentsImportService;
        }

        [FunctionName("Function")]
        public async Task Run([TimerTrigger("* * * * *")]TimerInfo myTimer, ILogger log) //("0 * * * *")
        {
            await _genericComponentsImportService.SaveNoiseComponents();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        }
    }
}
