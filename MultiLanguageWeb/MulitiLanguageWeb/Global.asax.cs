using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MultiLanguageWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			HttpCookie MyLang = Request.Cookies["MyLang"];
			if (MyLang != null)
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(MyLang.Value);
				System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(MyLang.Value);
			}
		}
	}
}
