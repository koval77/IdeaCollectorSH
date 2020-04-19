using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaCollectorSH.Models;

namespace IdeaCollectorSH.Controllers
{
    public class DocumentsController : Controller
    {
        private NewModelSH db = new NewModelSH();

        // GET: Documents
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.Idea);
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create(int? id)
        {
            ViewBag.IdeaID = new SelectList(db.Ideas, id, "IdeaTitle");
            ViewBag.Iid = id;
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload([Bind(Include = "DocumentID,IdeaID,DocumentName,PostedFile")] Document document)
        {
            if (document.PostedFile != null && document.PostedFile.ContentLength > 0)
                try
                {  //Server.MapPath takes the absolte path of folder 'Uploads'
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                               Path.GetFileName(document.PostedFile.FileName));
                    //Save file using Path+fileName take from above string
                    document.PostedFile.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            document.DocumentName = document.PostedFile.FileName;

            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

            var errors2 = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {     
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdeaID = new SelectList(db.Ideas, "IdeaID", "IdeaTitle", document.IdeaID);
            return RedirectToAction("Index","Home",document);
        }

        [HttpPost]
        public ActionResult StaryUpload(HttpPostedFileBase PostedFile)
        {
            if (PostedFile != null && PostedFile.ContentLength > 0)
                try
                {  //Server.MapPath takes the absolte path of folder 'Uploads'
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                               Path.GetFileName(PostedFile.FileName));
                    //Save file using Path+fileName take from above string
                    PostedFile.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Create","Documents");
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdeaID = new SelectList(db.Ideas, "IdeaID", "IdeaTitle", document.IdeaID);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentID,IdeaID,DocumentName")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdeaID = new SelectList(db.Ideas, "IdeaID", "IdeaTitle", document.IdeaID);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
