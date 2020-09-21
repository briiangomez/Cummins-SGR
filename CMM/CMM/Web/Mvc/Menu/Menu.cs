namespace CMM.Web.Mvc.Menu
{
    using CMM.Web.Mvc;
    using CMM.Web.Mvc.Menu.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class Menu : IMenuItemContainer
    {
        private static IDictionary<string, CMM.Web.Mvc.Menu.Menu> areasMenu = new Dictionary<string, CMM.Web.Mvc.Menu.Menu>(StringComparer.CurrentCultureIgnoreCase);
        private static CMM.Web.Mvc.Menu.Menu defaultMenu = null;

        static Menu()
        {
            defaultMenu = new CMM.Web.Mvc.Menu.Menu();
            MenuSection section = MenuSection.GetSection();
            if (section != null)
            {
                defaultMenu.ItemContainers = CreateItems(section.Items, new List<IMenuItemContainer>());
            }
        }

        public Menu()
        {
            this.Items = new MenuItem[0];
        }

        public static CMM.Web.Mvc.Menu.Menu BuildMenu(ControllerContext controllerContext)
        {
            string areaName = CMM.Web.Mvc.AreaHelpers.GetAreaName(controllerContext.RouteData);
            return BuildMenu(controllerContext, areaName);
        }

        public static CMM.Web.Mvc.Menu.Menu BuildMenu(ControllerContext controllerContext, string areaName)
        {
            CMM.Web.Mvc.Menu.Menu menu = new CMM.Web.Mvc.Menu.Menu();
            if (!(string.IsNullOrEmpty(areaName) || !areasMenu.ContainsKey(areaName)))
            {
                menu.Items = areasMenu[areaName].GetItems(controllerContext);
                return menu;
            }
            menu.Items = defaultMenu.GetItems(controllerContext);
            return menu;
        }

        private static IEnumerable<IMenuItemContainer> CreateItems(MenuItemElementCollection itemElementCollection, List<IMenuItemContainer> itemContainers)
        {
            if (itemElementCollection != null)
            {
                if (!string.IsNullOrEmpty(itemElementCollection.Type))
                {
                    itemContainers.Add((IMenuItemContainer) Activator.CreateInstance(Type.GetType(itemElementCollection.Type)));
                    return itemContainers;
                }
                foreach (MenuItemElement element in itemElementCollection)
                {
                    MenuItem item2 = new MenuItem {
                        Name = element.Name,
                        Text = element.Text,
                        Action = element.Action,
                        Controller = element.Controller,
                        Visible = element.Visible,
                        Area = element.Area,
                        RouteValues = new RouteValueDictionary(element.RouteValues.Attributes),
                        HtmlAttributes = new RouteValueDictionary(element.HtmlAttributes.Attributes),
                        ReadOnlyProperties = element.UnrecognizedProperties
                    };
                    MenuItem item = item2;
                    itemContainers.Add(item);
                    if (!string.IsNullOrEmpty(element.Initializer))
                    {
                        Type type = Type.GetType(element.Initializer);
                        item.Initializer = (IMenuItemInitializer) Activator.CreateInstance(type);
                    }
                    List<IMenuItemContainer> list = new List<IMenuItemContainer>();
                    if (element.Items != null)
                    {
                        item.ItemContainers = CreateItems(element.Items, list);
                    }
                }
            }
            return itemContainers;
        }

        public IEnumerable<MenuItem> GetItems(ControllerContext controllerContext)
        {
            List<MenuItem> list = new List<MenuItem>();
            if (this.ItemContainers != null)
            {
                foreach (IMenuItemContainer container in this.ItemContainers)
                {
                    if (container is MenuItem)
                    {
                        MenuItem item = (MenuItem) ((MenuItem) container).Clone();
                        item.Items = item.GetItems(controllerContext);
                        list.Add(item);
                    }
                    else
                    {
                        list.AddRange(container.GetItems(controllerContext));
                    }
                }
            }
            this.Items = list;
            this.Initialize(controllerContext);
            return list;
        }

        public void Initialize(ControllerContext controllerContext)
        {
            if (this.Items != null)
            {
                foreach (MenuItem item in this.Items)
                {
                    item.Initialize(controllerContext);
                }
            }
        }

        public static void RegisterAreaMenu(string area, string menuFileName)
        {
            lock (areasMenu)
            {
                MenuSection section = MenuSection.GetSection(menuFileName);
                if (section != null)
                {
                    CMM.Web.Mvc.Menu.Menu menu = new CMM.Web.Mvc.Menu.Menu {
                        ItemContainers = CreateItems(section.Items, new List<IMenuItemContainer>())
                    };
                    areasMenu.Add(area, menu);
                }
            }
        }

        internal IEnumerable<IMenuItemContainer> ItemContainers { get; set; }

        public IEnumerable<MenuItem> Items { get; set; }
    }
}

