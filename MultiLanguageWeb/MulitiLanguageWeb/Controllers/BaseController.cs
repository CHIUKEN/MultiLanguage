using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MultiLanguageWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        private static string _cookieLangName = "LangForMultiLanguageDemo";
        private static string _defaultCulture = "zh-tw";
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
			var langCookie = filterContext.HttpContext.Request.Cookies[_cookieLangName];
		
			string cultureOnCookie = langCookie?.Value;

            string cultureOnUrl = filterContext.RouteData.Values["culture"]?.ToString();

            string culture;

            if (cultureOnUrl != null)
            {
                culture = cultureOnUrl;
            }
            else if (cultureOnCookie != null)
            {
                culture = cultureOnCookie;
            }
            else
            {
                culture = _defaultCulture;
            }

            // Set the action parameter 
            filterContext.ActionParameters["culture"] = culture;

            var cultureInfo = CultureInfo.GetCultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
			
			if (cultureOnCookie == null)
            {
                langCookie = new HttpCookie(_cookieLangName, culture)
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMonths(6),
                };
                Response.AddHeader("Set-Cookie", "SameSite=Strict;Secure");
                Response.AppendCookie(langCookie);
            }
            else
            {
				langCookie.Value = culture;
				Response.AddHeader("Set-Cookie", "SameSite=Strict;Secure");
				Response.SetCookie(langCookie);
			}
            // Because we've overwritten the ActionParameters, we
            // make sure we provide the override to the 
            // base implementation.
            base.OnActionExecuting(filterContext);
        }

        public ActionResult RedirectToLocalized()
        {
            return RedirectPermanent("/zh-tw");
        }
    }
}