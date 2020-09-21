using Microsoft.AspNetCore.Mvc.Filters;

namespace SGR.Communicator
{
    public class CultureFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CMMController controller = filterContext.Controller as CMMController;
            if (!string.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name) && (controller != null)) //&& (CommunicatorContext.Current.Portal != null))
            {
                //LocalizationInfo localizationInfo = CommunicatorContext.Current.Portal.MessageContext.LocalizationInfo;
                //LocalizationInfo.CurrentCulture = localizationInfo.CultureInfo;
                //LocalizationInfo.CurrentEncoding = localizationInfo.Encoding;
                //LocalizationInfo.CurrentTimeZone = localizationInfo.TimeZoneInfo;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

