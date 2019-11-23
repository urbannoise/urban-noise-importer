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

            //Check the properties
            return genericComponentFirst.IdComponent == genericComponentSecond.IdComponent
                && genericComponentFirst.Coordinates.Latitude == genericComponentSecond.Coordinates.Latitude
                && genericComponentFirst.Coordinates.Longitude == genericComponentSecond.Coordinates.Longitude;
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
