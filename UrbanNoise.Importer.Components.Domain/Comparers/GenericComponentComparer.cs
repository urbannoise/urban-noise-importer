using System;
using System.Collections.Generic;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Domain.Comparers
{
    public class GenericComponentComparer : IEqualityComparer<GenericComponent>
    {
        public bool Equals(GenericComponent x, GenericComponent y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check the id is already stored in the database (coordinates are changing constantly so it is better to avoid it for now)
            return x.IdComponent == y.IdComponent;
        }

        public int GetHashCode(GenericComponent obj)
        {
            //Check whether the object is null
            if (obj is null) return 0;

            //Get hash code for the Name field if it is not null.
            int hashId = obj.IdComponent == null ? 0 : obj.IdComponent.GetHashCode();

            //Calculate the hash code for the product.
            return hashId;
        }
    }
}
