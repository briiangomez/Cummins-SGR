namespace CMM.Web.Mvc.Menu
{
    using System.Web.Mvc;

    public interface IMenuItemInitializer
    {
        MenuItem Initialize(MenuItem menuItem, ControllerContext controllerContext);
    }
}

