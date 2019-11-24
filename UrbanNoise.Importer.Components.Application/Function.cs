using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using UrbanNoise.Importer.Components.Business.Interfaces;

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
        public async Task Run([TimerTrigger("* * * * *")]TimerInfo myTimer, ILogger log) 
        {
            var result = await _genericComponentsImportService.SaveGenericNoiseComponents();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}.");
            log.LogInformation($"New components inserted: ${result.newComponentsInserted}");
            log.LogInformation($"Unused components deleted: ${result.unusedComponentsDeleted}");
        }
    }
}
