using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCollectorSH.Controllers
{
    public class MojController : Controller
    {
        // GET: Moj
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Moj()
        {
                 int hour = DateTime.Now.Hour;
                ViewBag.Message =
                    hour < 12 ? "Good morning , time is:" + DateTime.Now.ToShortTimeString()
                    : "Good afternoon, time is:" + DateTime.Now.ToShortTimeString();
                return View();
            }
    }
}