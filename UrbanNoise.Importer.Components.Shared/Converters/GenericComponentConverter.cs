using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                   UUID: Guid.NewGuid(),
                   IdComponent: i.IdComponent,
                   Coordinates: new Coordinates(i.Coordinates.SingleOrDefault().Latitude, i.Coordinates.SingleOrDefault().Longitude)
               )
           ).AsEnumerable();
        }

        public static GenericComponent MapToEntity(MapComponentDto mapComponentDto)
        {
            return null;
        }

        /*public static NoiseComponent MapToEntity(MapComponentDetailDto mapComponentDetailDto)
        {
            NoiseComponent noiseComponent = null;
            try
            {
                noiseComponent = new NoiseComponent
                {
                    IdComponent = mapComponentDetailDto.IdComponent,
                    UUID = Guid.NewGuid(),
                    Component = mapComponentDetailDto.ComponentName,
                    NoiseSensor = new NoiseSensor
                    {
                        Id = mapComponentDetailDto.SensorObservations.FirstOrDefault().Sensor,
                        DateLastUpdate = Convert.ToDateTime(mapComponentDetailDto.SensorObservations.FirstOrDefault().TimeStamp),
                        Found = mapComponentDetailDto.SensorObservations.FirstOrDefault().Found,
                        IsOnline = mapComponentDetailDto.SensorObservations.FirstOrDefault().SensorState == "online",
                        Unit = mapComponentDetailDto.SensorObservations.FirstOrDefault().Unit,
                        Value = Convert.ToDouble(mapComponentDetailDto.SensorObservations.FirstOrDefault().Value) //TODO: PEta aqui
                    }
                };
            }
            catch (Exception ex)
            {

            }
            return noiseComponent;
        }*/
    }
}
