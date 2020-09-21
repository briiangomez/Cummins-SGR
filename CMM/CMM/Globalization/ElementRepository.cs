namespace CMM.Globalization
{
    using System;
    using CMM.Globalization.Repository;

    public static class ElementRepository
    {
        public static IElementRepository DefaultRepository = new CacheElementRepository(new XmlElementRepository());
    }
}

