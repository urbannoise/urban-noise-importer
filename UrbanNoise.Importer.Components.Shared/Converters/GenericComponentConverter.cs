using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.ValueObjects;
using UrbanNoise.Importer.Components.Shared.Dtos;

namespace UrbanNoise.Importer.Components.Shared.Converters
{
    public static class GenericComponentConverter
    {
        public static IEnumerable<GenericComponent> MapToEntity(IEnumerable<MapComponentDto> mapComponentsDto)
        {
            return mapComponentsDto.Select(i =>
               new GenericComponent(
                   ObjectId: ObjectId.GenerateNewId(),
                   IdComponent: i.IdComponent,
                   Coordinates: new Coordinates(i.Coordinates.Latitude, i.Coordinates.Longitude)
               )
           ).AsEnumerable();
        }
    }
}
