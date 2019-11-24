using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Shared.Dtos;

namespace UrbanNoise.Importer.Components.Business.Interfaces
{
    public interface IGenericComponentsImportService
    {
        Task<IEnumerable<GenericComponent>> ImportGenericComponents();
        Task<(IEnumerable<GenericComponent> componentsToInsert, IEnumerable<GenericComponent> componentsToDelete)> GenericComponentsHaveChanged(IEnumerable<GenericComponent> genericComponents);
        IEnumerable<GenericComponent> GetGenericNoiseComponents(MapComponentsDto mapComponentsDto);
        Task<(bool newComponentsInserted, bool unusedComponentsDeleted)> SaveGenericNoiseComponents();
    }
}
