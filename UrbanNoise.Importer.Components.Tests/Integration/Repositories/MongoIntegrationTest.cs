using Mongo2Go;
using MongoDB.Driver;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Tests.Integration.Repositories
{
    public static class MongoIntegrationTest
    {
        internal static MongoDbRunner _runner;
        internal static IMongoCollection<GenericComponent> _collection;

        internal static void CreateConnection()
        {
            _runner = MongoDbRunner.Start();

            IMongoClient client = new MongoClient(_runner.ConnectionString);
            IMongoDatabase database = client.GetDatabase("IntegrationTest");
            _collection = database.GetCollection<GenericComponent>("TestCollection");
        }
    }
}
