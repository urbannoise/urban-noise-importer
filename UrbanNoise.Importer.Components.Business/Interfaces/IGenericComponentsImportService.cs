using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Business.Interfaces
{
    public interface IGenericComponentsImportService
    {
        Task<IEnumerable<GenericComponent>> ImportGenericComponents();
    }
}
