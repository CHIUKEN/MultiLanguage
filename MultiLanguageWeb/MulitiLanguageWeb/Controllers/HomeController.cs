using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiLanguageWeb.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string culture)
        {
            var langCookie = Request.Cookies["LangForMultiLanguageDemo"];

            if (langCookie == null)
            {
                langCookie = new HttpCookie("LangForMultiLanguageDemo", culture)
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

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}