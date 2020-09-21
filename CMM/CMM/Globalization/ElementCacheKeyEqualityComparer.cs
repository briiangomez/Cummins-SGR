namespace CMM.Globalization
{
    using System;
    using System.Collections.Generic;

    public class ElementCacheKeyEqualityComparer : IEqualityComparer<ElementCacheKey>
    {
        public bool Equals(ElementCacheKey x, ElementCacheKey y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(ElementCacheKey obj)
        {
            ElementCacheKey key = obj;
            return key.Hash;
        }
    }
}

