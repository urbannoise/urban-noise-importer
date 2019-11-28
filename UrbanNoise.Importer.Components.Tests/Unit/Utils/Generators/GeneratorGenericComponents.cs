using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.ValueObjects;

namespace UrbanNoise.Importer.Components.Tests.Unit.Utils.Generators
{
    public static class GeneratorGenericComponents
    {
        public static IEnumerable<GenericComponent> GenerateGenericComponents()
        {
            var components =  new List<GenericComponent>
            {
                new GenericComponent
                {
                    Id = ObjectId.GenerateNewId(),
                    IdComponent = "1",
                    Coordinates = new Coordinates
                    {
                        Longitude = "4.1222",
                        Latitude = "0.22213"
                    }
                },
                new GenericComponent
                {
                    Id = ObjectId.GenerateNewId(),
                    IdComponent = "2",
                    Coordinates = new Coordinates
                    {
                        Longitude = "8.1222",
                        Latitude = "1.22213"
                    }
                }
            };
            return components;
        }

        public static Task<IEnumerable<GenericComponent>> GenerateGenericComponentsAsync()
        {
            return Task.FromResult(GenerateGenericComponents());
        }

        public static GenericComponent GenerateGenerateComponent()
        {
            return new GenericComponent
            {
                Id = ObjectId.GenerateNewId(),
                IdComponent = "3",
                Coordinates = new Coordinates
                {
                    Longitude = "4.1222",
                    Latitude = "0.22213"
                }
            };
        }
    }
}
