using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaCollectorSH.Controllers
{
    public class QAManagerController : Controller
    {
        [Authorize(Roles ="QAManager")]
        // GET: QAManager
        public ActionResult Index()
        {
            return View();
        }

        // GET: QAManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QAManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QAManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QAManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QAManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QAManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QAManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
