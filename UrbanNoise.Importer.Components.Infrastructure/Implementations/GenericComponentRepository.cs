using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;
using UrbanNoise.Importer.Components.Shared.Settings;

namespace UrbanNoise.Importer.Components.Infrastructure.Implementations
{
    public class GenericComponentRepository : IGenericComponentRepository
    {
        private readonly IMongoCollection<GenericComponent> _genericComponentsCollection;

        public GenericComponentRepository(IOptions<AppSettings> options)
        {
            AppSettings _appSettings = options.Value;
            IMongoClient _client = new MongoClient(_appSettings.CosmosDb.ConnectionString);
            IMongoDatabase _database = _client.GetDatabase(_appSettings.CosmosDb.Database);
            
            _genericComponentsCollection = _database.GetCollection<GenericComponent>(_appSettings.CosmosDb.Collection);
        }

        public async Task<IEnumerable<GenericComponent>> GetGenericComponents()
        {
            return await _genericComponentsCollection.Find<GenericComponent>(new BsonDocument()).ToListAsync();
        }

        public async Task<IEnumerable<GenericComponent>> SaveGenericComponents(IEnumerable<GenericComponent> genericComponents)
        {
            await _genericComponentsCollection.InsertManyAsync(genericComponents);
            return genericComponents;
        }

        public async Task<long> DeleteGenericComponents(IEnumerable<GenericComponent> genericComponents)
        {         
            var result = await _genericComponentsCollection.DeleteManyAsync(ItemWithListOfId(genericComponents.Select(i => i.Id).ToList()));
            return result.DeletedCount;
        }

        protected FilterDefinition<GenericComponent> ItemWithListOfId(List<ObjectId> id)
        {
            return Builders<GenericComponent>.Filter.In("_id", id);
        }
    }
}
