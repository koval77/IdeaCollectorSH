using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCollectorSH.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

  /*      public ActionResult Moj()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Message =
                hour < 12 ? "Good morning , time is:" + DateTime.Now.ToShortTimeString()
                : "Good afternoon, time is:" + DateTime.Now.ToShortTimeString();
            return View();
        }*/
    }
}