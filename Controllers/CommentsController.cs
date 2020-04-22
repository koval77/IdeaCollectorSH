using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaCollectorSH.Models;

namespace IdeaCollectorSH.Controllers
{
    public class CommentsController : Controller
    {
        NewModelSH db = new NewModelSH();

        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comments/Create
        public ActionResult Create(int? id)
        {
            Idea thisIdea = db.Ideas.Find(id);
            //string ideaEmail = thisIdea.AuthorEmail;
            ViewBag.IdeaId = id;
            ViewBag.IdeaTitle = thisIdea.IdeaTitle;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="CommentContent,IdeaID")] Comment comment)
        {
            //IList<Staff> tempstafflist = db.Staffs.ToList();
            //var mystaff = from s in tempstafflist where s.Email == User.Identity.Name select s;
            var mystaff=db.Staffs.FirstOrDefault(x => x.Email == User.Identity.Name);

            comment.StaffID = mystaff.StaffID;
            comment.SubmitDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            //ViewBag.IdeaID = new SelectList(db.Ideas, "IdeaID", "IdeaTitle", document.IdeaID);
            return RedirectToAction("Index", "Home");
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createold(FormCollection collection)
        {
            try
            {
                IList<Staff> tempstafflist = db.Staffs.ToList();
                Comment thiscomment = new Comment();
                thiscomment.CommentContent = collection[1];
                thiscomment.IdeaID = Convert.ToInt16(collection[2]);
                var wantedStaffID = from s in tempstafflist where s.Email == User.Identity.Name select s.StaffID;
                thiscomment.StaffID = Convert.ToInt16(wantedStaffID);

                //var thread = db.Threads.FirstOrDefault(x => x.ThreadID == id);
                //string content = Convert.ToString(collection[0]);
                //int ideaId =Convert.ToInt16(collection[1]);
                
                //var wantedStaffID = from s in tempstafflist where s.Email == User.Identity.Name select s.StaffID;
                
                //thiscomment.CommentContent = content;
                thiscomment.SubmitDate = DateTime.Now;
               //thiscomment.IdeaID = ideaId;
                //thiscomment.StaffID = wantedStaffID;
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comments/Edit/5
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

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comments/Delete/5
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
