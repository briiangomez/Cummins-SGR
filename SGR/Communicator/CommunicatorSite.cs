using CMM.Globalization;
using CMM.Web.Url;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SGR.Communicator.Models;

namespace SGR.Communicator
{    
    public static class CommunicatorSite
    {
        public static IDictionary<string, string> AllowedCultures;
        public const int DefaultErrorTimeOut = 20;
        public const int DefaultInfoTimeOut = 5;
        public const int DefaultMessageTimeout = 0x1388;
        public const int DefaultPageSize = 10;
        public const int DefaultWarngingTimeOut = 10;
        public static IList<MenuItem> FullMenus;
        public static IDictionary<string, MenuItem> FullMenusDictionary;
        public const int ImportantWarngingTimeOut = 60;

        static CommunicatorSite()
        {
            InitCulture();
            InitMenu();
        }

        //private static void AddSubMenu(UrlHelper uh, string baseId, IList<MenuItem> list, IEnumerable<XElement> els)
        //{
        //    foreach (XElement element in els)
        //    {
        //        MenuItem item2 = new MenuItem {
        //            Id = baseId + element.Attribute("id").Value,
        //            Text = element.Attribute("text").Value,
        //            Url = uh.Action(element.Attribute("action").Value, element.Attribute("controller").Value)
        //        };
        //        MenuItem item = item2;
        //        if (element.Attribute("class") != null)
        //        {
        //            item.Class = element.Attribute("class").Value;
        //        }
        //        if (element.Attribute("group") != null)
        //        {
        //            item.Group = element.Attribute("group").Value;
        //        }
        //        if (element.Attribute("needAdmin") != null)
        //        {
        //            item.NeedAdmin = element.Attribute("needAdmin").Value == "true";
        //        }
        //        if (element.Elements().Count<XElement>() > 0)
        //        {
        //            AddSubMenu(uh, item.Id + "_", item.SubMenus, element.Elements());
        //        }
        //        list.Add(item);
        //        FullMenusDictionary.Add(item.Id, item);
        //    }
        //}

       
        private static void InitCulture()
        {
            //AllowedCultures = new Dictionary<string, string>();            
            //XDocument document = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/CMM.Communicator.App_Data.Languages.xml"));
            //foreach (XElement element in document.Root.Elements())
            //{
            //    string key = element.Element("Name").Value;
            //    string source = element.Element("DisplayName").Value;
            //    if (AllowedCultures.ContainsKey(key))
            //    {
            //        if (!(!AllowedCultures[key].Contains<char>('(') || source.Contains<char>('(')))
            //        {
            //            AllowedCultures[key] = source;
            //        }
            //    }
            //    else
            //    {
            //        AllowedCultures.Add(key, source);
            //    }
            //}
        }

        private static void InitMenu()
        {
            //UrlHelper uh = new UrlHelper(HttpContext.Current.Request.RequestContext);
            //FullMenus = new List<MenuItem>();
            //FullMenusDictionary = new Dictionary<string, MenuItem>();            
            //XDocument document = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/CMM.Communicator.App_Data.Menus.ch.xml"));
            //AddSubMenu(uh, string.Empty, FullMenus, document.Root.Elements());
        }

        public static string PageTitle(string subTitle)
        {
            string str = "CMM Communicator".Localize("");
            if (string.IsNullOrEmpty(subTitle))
            {
                return str;
            }
            return (str + "-" + subTitle);
        }
    }
}

