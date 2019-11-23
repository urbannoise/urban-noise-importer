using Microsoft.Azure.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Infrastructure.Implementations
{
    public class GenericComponentRepository : IGenericComponentRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private readonly AppSettings _appSettings;
        private readonly IMongoCollection<GenericComponent> _genericComponentsCollection;

        public GenericComponentRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
            _client = new MongoClient(_appSettings.CosmosDb.ConnectionString);
            _database = _client.GetDatabase(_appSettings.CosmosDb.Database);
            _genericComponentsCollection = _database.GetCollection<GenericComponent>(_appSettings.CosmosDb.Collection);
        }

        public async Task<IEnumerable<GenericComponent>> GetGenericComponents()
        {
            return await _genericComponentsCollection.Find<GenericComponent>(new BsonDocument()).ToListAsync();
        }

        public async Task SaveGenericComponents(IEnumerable<GenericComponent> genericComponents)
        {
            await _genericComponentsCollection.InsertManyAsync(genericComponents);
        }
    }
}
