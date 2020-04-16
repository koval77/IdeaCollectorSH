using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaCollectorSH.Models;
using System.Data.Entity;

namespace IdeaCollectorSH.Controllers
{
    
    public class HomeController : Controller
    {
        NewModelSH db = new NewModelSH();
        public ActionResult Index()
        {
            var ideas = db.Ideas.Include(i => i.Staff);
            return View(ideas.ToList());
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