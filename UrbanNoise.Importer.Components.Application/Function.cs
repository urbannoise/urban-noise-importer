using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Application
{
    public class Function
    {
        private readonly AppSettings appSettings;
        public Function(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
        }
        [FunctionName("Function")]
        public void Run([TimerTrigger("* * * * *")]TimerInfo myTimer, ILogger log) //("0 * * * *")
        {
            var x = appSettings.Database.ConnectionString;
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        }
    }
}
