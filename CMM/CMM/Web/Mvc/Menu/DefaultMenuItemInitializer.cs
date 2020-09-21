namespace CMM.Web.Mvc.Menu
{
    using CMM.Web.Mvc;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class DefaultMenuItemInitializer : IMenuItemInitializer
    {
        protected virtual bool GetIsActive(MenuItem menuItem, ControllerContext controllerContext)
        {
            if (!string.IsNullOrEmpty(menuItem.Area) && (string.Compare(menuItem.Area, CMM.Web.Mvc.AreaHelpers.GetAreaName(controllerContext.RouteData)) != 0))
            {
                return false;
            }
            return (string.Compare(controllerContext.RouteData.Values["controller"].ToString(), menuItem.Controller, true) == 0);
        }

        protected virtual bool GetIsVisible(MenuItem menuItem, ControllerContext controllerContext)
        {
            return true;
        }

        public virtual MenuItem Initialize(MenuItem menuItem, ControllerContext controllerContext)
        {
            string areaName = CMM.Web.Mvc.AreaHelpers.GetAreaName(controllerContext.RouteData);
            if (!string.IsNullOrEmpty(areaName))
            {
                menuItem.RouteValues["area"] = areaName;
            }
            if (!string.IsNullOrEmpty(menuItem.Area))
            {
                menuItem.RouteValues["area"] = menuItem.Area;
            }
            bool isActive = this.GetIsActive(menuItem, controllerContext);
            foreach (MenuItem item in menuItem.Items)
            {
                item.Initialize(controllerContext);
                isActive = isActive || item.IsActive;
            }
            menuItem.IsActive = isActive;
            bool isVisible = this.GetIsVisible(menuItem, controllerContext);
            if (string.IsNullOrEmpty(menuItem.Action) && ((from it in menuItem.Items
                where it.Visible
                select it).Count<MenuItem>() == 0))
            {
                isVisible = false;
            }
            menuItem.Visible = isVisible;
            return menuItem;
        }
    }
}

