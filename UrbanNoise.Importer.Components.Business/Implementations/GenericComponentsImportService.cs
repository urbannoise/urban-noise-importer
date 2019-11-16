using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Business.Interfaces;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;

namespace UrbanNoise.Importer.Components.Business.Implementations
{
    public class GenericComponentsImportService : IGenericComponentsImportService
    {
        private readonly IGenericComponentRepository _genericComponentRepository;

        public GenericComponentsImportService(IGenericComponentRepository genericComponentRepository)
        {
            _genericComponentRepository = genericComponentRepository;
        }


        public Task<IEnumerable<GenericComponent>> ImportGenericComponents()
        {
            throw new NotImplementedException();
        }
    }
}
