namespace CMM.Web.Mvc.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MenuItem : IMenuItemContainer, ICloneable
    {
        private IMenuItemInitializer menuItemInitiaizer = new DefaultMenuItemInitializer();

        public MenuItem()
        {
            this.Items = new MenuItem[0];
        }

        public object Clone()
        {
            MenuItem item = (MenuItem) base.MemberwiseClone();
            item.RouteValues = new RouteValueDictionary(this.RouteValues);
            item.HtmlAttributes = new RouteValueDictionary(this.HtmlAttributes);
            return item;
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
                        MenuItem item = (MenuItem) container;
                        list.Add(item);
                        item.Items = item.GetItems(controllerContext);
                    }
                    else
                    {
                        list.AddRange(container.GetItems(controllerContext));
                    }
                }
            }
            return list;
        }

        public virtual IMenuItemContainer Initialize(ControllerContext controllerContext)
        {
            return this.menuItemInitiaizer.Initialize(this, controllerContext);
        }

        public string Action { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public RouteValueDictionary HtmlAttributes { get; set; }

        public virtual IMenuItemInitializer Initializer
        {
            get
            {
                return this.menuItemInitiaizer;
            }
            set
            {
                this.menuItemInitiaizer = value;
            }
        }

        public bool IsActive { get; set; }

        internal IEnumerable<IMenuItemContainer> ItemContainers { get; set; }

        public virtual IEnumerable<MenuItem> Items { get; set; }

        public string Name { get; set; }

        public NameValueCollection ReadOnlyProperties { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public string Text { get; set; }

        public bool Visible { get; set; }
    }
}

