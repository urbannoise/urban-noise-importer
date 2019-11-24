using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Shared.Dtos;

namespace UrbanNoise.Importer.Components.Tests.Unit.Utils.Generators
{
    public static class GeneratorMapComponentsDto
    {
        public static MapComponentsDto GenerateMapComponentsDto()
        {
            return new MapComponentsDto
            {
                Components = new List<MapComponentDto>
                {
                    new MapComponentDto
                    {
                        Icon = "noise",
                        ComponentType = "noise",
                        IdComponent = "noise",
                        Coordinates = new CoordinateDto
                        {
                            Latitude = "4.2113",
                            Longitude = "0.1233"
                        }
                    },
                    new MapComponentDto
                    {
                        Icon = "water",
                        ComponentType = "water",
                        IdComponent = "water",
                        Coordinates = new CoordinateDto
                        {
                            Latitude = "4.2113",
                            Longitude = "0.1233"
                        }
                    }
                }
            };
        }

        public static MapComponentsDto GenerateWrongMapComponentsDto()
        {
            return new MapComponentsDto
            {
                Components = new List<MapComponentDto>
                {
                    new MapComponentDto
                    {
                        Icon = "water",
                        ComponentType = "water",
                        IdComponent = "water",
                        Coordinates = new CoordinateDto
                        {
                            Latitude = "4.2113",
                            Longitude = "0.1233"
                        }
                    }
                }
            };
        }

        public static Task<MapComponentsDto> GenerateMapComponentsDtoAsync()
        {
            return Task.FromResult(GenerateMapComponentsDto());
        }

        public static Task<MapComponentsDto> GenerateWrongMapComponentsDtoAsync()
        {
            return Task.FromResult(GenerateWrongMapComponentsDto());
        }
    }
}
