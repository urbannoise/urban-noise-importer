using System;

namespace UrbanNoise.Importer.Components.Tests.Integration.Utils
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope() { }

        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}
