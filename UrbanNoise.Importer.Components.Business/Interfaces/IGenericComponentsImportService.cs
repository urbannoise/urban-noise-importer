using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Shared.Dtos;

namespace UrbanNoise.Importer.Components.Business.Interfaces
{
    public interface IGenericComponentsImportService
    {
        Task<IEnumerable<GenericComponent>> ImportGenericComponents();
        Task<Boolean> GenericComponentsHaveChanged(IEnumerable<GenericComponent> genericComponents);
        IEnumerable<GenericComponent> GetGenericNoiseComponents(MapComponentsDto mapComponentsDto);
        Task SaveGenericNoiseComponents();
    }
}
