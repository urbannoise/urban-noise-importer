using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Domain.Repositories
{
    public interface IGenericComponentRepository
    {
        Task<IEnumerable<GenericComponent>> GetGenericComponents();

        Task SaveGenericComponents(IEnumerable<GenericComponent> genericComponents);
    }
}
