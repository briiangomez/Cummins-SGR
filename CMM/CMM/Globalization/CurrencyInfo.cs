namespace CMM.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class CurrencyInfo
    {
        private static List<CurrencyInfo> _Currencies;

        public static IEnumerable<CurrencyInfo> GetCurrencies()
        {
            if (_Currencies == null)
            {
                lock (typeof(CurrencyInfo))
                {
                    if (_Currencies == null)
                    {
                        CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures | CultureTypes.SpecificCultures);
                        List<CurrencyInfo> source = new List<CurrencyInfo>();
                        foreach (CultureInfo info in cultures)
                        {
                            try
                            {
                                RegionInfo region = new RegionInfo(info.LCID);
                                if (!source.Any<CurrencyInfo>(i => (i.EnglishName == region.CurrencyEnglishName)))
                                {
                                    CurrencyInfo item = new CurrencyInfo {
                                        EnglishName = region.CurrencyEnglishName,
                                        NativeName = region.CurrencyNativeName,
                                        Symbol = region.CurrencySymbol,
                                        ISOSymbol = region.ISOCurrencySymbol
                                    };
                                    source.Add(item);
                                }
                            }
                            catch
                            {
                            }
                        }
                        _Currencies = source;
                    }
                }
            }
            return _Currencies;
        }

        public static CurrencyInfo GetCurrencyInfo(string englishName)
        {
            return (from i in GetCurrencies()
                where i.EnglishName == englishName
                select i).FirstOrDefault<CurrencyInfo>();
        }

        public static CurrencyInfo GetCurrencyInfoByISOSymbol(string isoSymbol)
        {
            return (from i in GetCurrencies()
                where i.ISOSymbol == isoSymbol
                select i).FirstOrDefault<CurrencyInfo>();
        }

        public string EnglishName { get; set; }

        public string ISOSymbol { get; set; }

        public string NativeName { get; set; }

        public string Symbol { get; set; }
    }
}

