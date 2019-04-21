using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreelancerMarketplace;

namespace FreelancerMarketplace.Controllers
{
    public class FreelancersController : Controller
    {
        private DB66Entities db = new DB66Entities();

        // GET: Freelancers
        public ActionResult Index()
        {
            var freelancers = db.Freelancers.Include(f => f.Category).Include(f => f.Lookup).Include(f => f.Lookup1);
            return View(freelancers.ToList());
        }

        // GET: Freelancers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // GET: Freelancers/Create
        public ActionResult AddInfo()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups, "Id", "value");
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value");
            return View();
        }

        // POST: Freelancers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInfo([Bind(Include = "FreelancerId,CategoryId,ExperienceInCategory,ProfessionalTitle,ProfessionalOverview,JobType")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Freelancers.Add(freelancer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", freelancer.CategoryId);
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups, "Id", "value", freelancer.ExperienceInCategory);
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value", freelancer.JobType);
            return View(freelancer);
        }

        // GET: Freelancers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", freelancer.CategoryId);
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups, "Id", "value", freelancer.ExperienceInCategory);
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value", freelancer.JobType);
            return View(freelancer);
        }

        // POST: Freelancers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FreelancerId,CategoryId,ExperienceInCategory,ProfessionalTitle,ProfessionalOverview,JobType")] Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freelancer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", freelancer.CategoryId);
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups, "Id", "value", freelancer.ExperienceInCategory);
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value", freelancer.JobType);
            return View(freelancer);
        }

        // GET: Freelancers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelancer freelancer = db.Freelancers.Find(id);
            if (freelancer == null)
            {
                return HttpNotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Freelancer freelancer = db.Freelancers.Find(id);
            db.Freelancers.Remove(freelancer);
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
