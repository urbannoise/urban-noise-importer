using System;
using System.Collections.Generic;
using System.Text;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Domain.Repositories
{
    public interface INoiseComponentRepository
    {
        void SaveNoiseComponent(IEnumerable<NoiseComponent> noiseComponents);
    }
}
