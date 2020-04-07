using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace IdeaCollectorSH.Controllers
{
    public class MojController : Controller
    {
        // string collection
        IList<string> stringList = new List<string>() {
    "Wlochaty",
    "Guernica",
    "Dezerter",
    "MVC Tutorials jebane" ,
    "Psy wojny",
    "KSU",
    "Smar SW",
    "Disaffect",
    "Turboreanjimacja",
    "Kastracja",
    "Disfear",
    "Disorder",
    "Chaos UK"
};



        // GET: Moj
        public ActionResult Index()
        {
            // LINQ Query Syntax
            var result = from s in stringList
                         where s.Contains("Dis")
                         select s;
            ViewBag.Message = result.FirstOrDefault();
            ViewBag.Ile = stringList.Count();
            ViewBag.My = result.First();
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