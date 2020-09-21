namespace CMM.Web.Mvc
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class AutoCompleteController : Controller
    {
        public ActionResult Index(string dataSourceType, string term)
        {
            if (string.IsNullOrEmpty(dataSourceType))
            {
                throw new ArgumentNullException("The dataSourceType can not be null.");
            }
            Type type = Type.GetType(dataSourceType);
            if (type == null)
            {
                throw new Exception(string.Format("The type of \"{0}\" can not be found.", dataSourceType));
            }
            ISelectListDataSource source = (ISelectListDataSource) Activator.CreateInstance(type);
            return base.Json((from it in source.GetSelectListItems(base.ControllerContext.RequestContext, term) select new { label = it.Text, category = it.Value }).ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}

