using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanNoise.Importer.Components.Domain.Entities;
using UrbanNoise.Importer.Components.Domain.Repositories;

namespace UrbanNoise.Importer.Components.Infrastructure.Implementations
{
    public class GenericComponentRepository : IGenericComponentRepository
    {
        private readonly IDocumentClient _documentClient;
        public GenericComponentRepository(IDocumentClient documentClient)
        {
            _documentClient = documentClient;
        }


        public async Task DeleteGenericComponents()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenericComponent>> GetGenericComponents()
        {
            throw new NotImplementedException();
        }

        public async Task SaveGenericComponents(IEnumerable<GenericComponent> genericComponents)
        {
            throw new NotImplementedException();
        }
    }
}
