using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using UrbanNoise.Importer.Components.Business.Implementations;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Infrastructure.Implementations;
using UrbanNoise.Importer.Components.Shared.Settings;

[assembly: FunctionsStartup(typeof(UrbanNoise.Importer.Components.Application.Startup))]

namespace UrbanNoise.Importer.Components.Application
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<AppSettings>(configuration);

            builder.Services.AddSingleton<IDocumentClient>(serviceProvider => {
                 DbConnectionStringBuilder cosmosDBConnectionStringBuilder = new DbConnectionStringBuilder
                 {
                     ConnectionString = Environment.GetEnvironmentVariable("Database:ConnectionString")
                 };

                 if (cosmosDBConnectionStringBuilder.TryGetValue("AccountKey", out object accountKey) && cosmosDBConnectionStringBuilder.TryGetValue("AccountEndpoint", out object accountEndpoint))
                 {
                     return new DocumentClient(new Uri(accountEndpoint.ToString()), accountKey.ToString());

                 }

                 return null;
             });

            builder.Services.AddLogging();

            builder.Services.AddTransient<IGenericComponentsImportService, GenericComponentsImportService>();
            builder.Services.AddSingleton<IGenericComponentRepository, GenericComponentRepository>();
        }
    }
}
