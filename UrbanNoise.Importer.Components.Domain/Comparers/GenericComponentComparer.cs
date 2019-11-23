using System;
using System.Collections.Generic;
using System.Text;
using UrbanNoise.Importer.Components.Domain.Entities;

namespace UrbanNoise.Importer.Components.Domain.Comparers
{
    public class GenericComponentComparer : IEqualityComparer<GenericComponent>
    {
        public bool Equals(GenericComponent genericComponentFirst, GenericComponent genericComponentSecond)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(genericComponentFirst, genericComponentSecond)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(genericComponentFirst, null) || Object.ReferenceEquals(genericComponentSecond, null))
                return false;

            //Check the properties
            var propertiesAreTheSame = genericComponentFirst.IdComponent == genericComponentSecond.IdComponent
                && genericComponentFirst.Coordinates.Latitude == genericComponentSecond.Coordinates.Latitude
                && genericComponentFirst.Coordinates.Longitude == genericComponentSecond.Coordinates.Longitude;

            if (propertiesAreTheSame)
                return true;
            else
                return false;
        }

        public bool Equals(GenericComponent other)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(GenericComponent obj)
        {
            throw new NotImplementedException();
        }
    }


    public class GenericComponentsComparer : IEqualityComparer<IEnumerable<GenericComponent>>
    {
        public bool Equals(GenericComponent genericComponentFirst, GenericComponent genericComponentSecond)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(genericComponentFirst, genericComponentSecond)) return true;

            //Check the properties
            return genericComponentFirst.IdComponent == genericComponentSecond.IdComponent
                && genericComponentFirst.Coordinates.Latitude == genericComponentSecond.Coordinates.Latitude
                && genericComponentFirst.Coordinates.Longitude == genericComponentSecond.Coordinates.Longitude;
        }

        public bool Equals(IEnumerable<GenericComponent> x, IEnumerable<GenericComponent> y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(GenericComponent obj)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IEnumerable<GenericComponent> obj)
        {
            throw new NotImplementedException();
        }
    }
}
