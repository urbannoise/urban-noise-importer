using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Domain.Repositories
{
    public interface IGenericComponentRepository
    {
        Task<IEnumerable<GenericComponent>> GetGenericComponents();

        Task<IEnumerable<GenericComponent>> SaveGenericComponents(IEnumerable<GenericComponent> genericComponents);

        Task<long> DeleteGenericComponents(IEnumerable<GenericComponent> genericComponents);
    }
}
