namespace CMM.Web.Mvc.Menu
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IMenuItemContainer
    {
        IEnumerable<MenuItem> GetItems(ControllerContext controllerContext);
    }
}

