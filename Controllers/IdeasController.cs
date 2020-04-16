using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaCollectorSH.Models;
using System.Security.Authentication;
using System.Net.Mail;
using PagedList;

namespace IdeaCollectorSH.Controllers
{
    public class IdeasController : Controller
    {
        private NewModelSH db = new NewModelSH();
        private ApplicationDbContext appdbctx = new ApplicationDbContext();

        // GET: Ideas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LikesSortParm = sortOrder == "Likes" ? "like_asc" : "like_desc";
            ViewBag.DislikesSortParm = sortOrder == "Dislikes" ? "disl_asc" : "disl_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ideas = from s in db.Ideas.Include(i=>i.Staff)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ideas = ideas.Where(s => s.IdeaTitle.Contains(searchString)
                                       || s.IdeaDescription.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ideas = ideas.OrderByDescending(s => s.IdeaTitle);
                    break;
                case "Date":
                    ideas = ideas.OrderBy(s => s.SubmitDate);
                    break;
                case "date_desc":
                    ideas = ideas.OrderByDescending(s => s.SubmitDate);
                    break;
                case "like_asc":
                    ideas = ideas.OrderBy(s => s.TUp);
                    break;
                case "like_desc":
                    ideas = ideas.OrderByDescending(s => s.TUp);
                    break;
                case "disl_asc":
                    ideas = ideas.OrderBy(s => s.TDown);
                    break;
                case "disl_desc":
                    ideas = ideas.OrderByDescending(s => s.TDown);
                    break;
                default:
                    ideas = ideas.OrderBy(s => s.SubmitDate);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(ideas.ToPagedList(pageNumber, pageSize));
            //ideas = db.Ideas.Include(i => i.Staff);
        }

        // GET: Ideas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // GET: Ideas/Create
        public ActionResult Create()
        {
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName");
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdeaID,IdeaTitle,SubmitDate,Category,TUp,TDown,ExpiryDate,ViewCount,StaffID,IdeaDescription,AuthorEmail")] Idea idea)
        {

            if (ModelState.IsValid)
            {
                var senderEmail = new MailAddress("kwojtek621@gmail.com", User.Identity.Name);
                var receiverEmail = new MailAddress("kwojtek621@gmail.com", "Receiver");
                var password = "cipka6977";
                var sub = "The idea was just submitted";
                var body = "Hello. We want to inform to inform you that a new idea was submitted recently. The author of this idea is:"+User.Identity.Name+". The idea was submitted at: "+DateTime.Now;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                idea.SubmitDate = DateTime.Now;
                idea.ExpiryDate = DateTime.Now.AddYears(1);
                idea.AuthorEmail = User.Identity.Name;
                db.Ideas.Add(idea);
                db.SaveChanges();
                //client.Send(mail);

                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", idea.StaffID);
            ViewBag.CurrentUser = User.ToString();
            return View(idea);
        }

        // GET: Ideas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", idea.StaffID);
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdeaID,IdeaTitle,SubmitDate,Category,TUp,TDown,ExpiryDate,ViewCount,StaffID,IdeaDescription")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", idea.StaffID);
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idea idea = db.Ideas.Find(id);
            db.Ideas.Remove(idea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            //Console.WriteLine(base.RouteData.Values);
            base.Dispose(disposing);
        }

        public ActionResult ThumbUp(int? id)
        {
            Console.WriteLine("The id will be then: " + id);
            TempData.Add("MyTempData", id);
            return RedirectToAction("ThumbUpPost", id);
        }

        [HttpPost]
        public ActionResult ThumbUpPost()
        {
            string IdeaId = Request.Form["UserId"];
            int ideaIdnumber = Convert.ToInt32(IdeaId);


            Idea idea = db.Ideas.Find(ideaIdnumber);
            if (idea.TUp == null)
            {
                idea.TUp = 0;
            }
            else
            {
                idea.TUp += 1;
            }
            

            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(idea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThumbDownPost()
        {
            string IdeaId = Request.Form["UserId"];
            int ideaIdnumber = Convert.ToInt32(IdeaId);


            Idea idea = db.Ideas.Find(ideaIdnumber);
            if (idea.TDown == null)
            {
                idea.TDown = 0;
            }
            else
            {
                idea.TDown += 1;
            }


            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(idea);
        }
    }
}
