using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreelancerMarketplace;
using FreelancerMarketplace.Models;
using Microsoft.AspNet.Identity;

namespace FreelancerMarketplace.Controllers
{
    public class EmployerController : Controller
    {
        private DB66Entities db = new DB66Entities();
        // GET: Employer
        public ActionResult Dashboard()
        {
            return View();
        }
        
        // GET: Employer/Details/5
        public ActionResult ManageBidders(int id)
        {
            return View();
        }
        public ActionResult Index()
        {
            var employer = db.Employers.Include(e => e.Company);
            return View(employer.ToList());
        }


        // GET: Employer/Messages
        public ActionResult Messages()
        {
            return View();
        }


        public ActionResult MyJobs()
        {
            string id = User.Identity.GetUserId();
            int empId = db.People.FirstOrDefault(p => p.User_AccountID.Equals(id)).PersonId;

            List<Job> Myjobs = new List<Job>();
            foreach (Job job in db.Jobs)
            {
                if (job.EmployerID == empId)
                {
                    Myjobs.Add(job);
                }
            }
            return View(Myjobs);
        }

        // POST: Employer/Create
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

        // GET: Employer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description, PaymentAmonut, AttatchmentId, Expiry, SkillID, EmployerID, TimePosted, CategoryId, Deadline")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyJobs");
            }
            return View(job);
        }

        // GET: Employer/DeleteJob/5
        public ActionResult DeleteJob(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        
        [HttpPost, ActionName("DeleteJob")]
        [ValidateAntiForgeryToken]
        public ActionResult JobDeleted(int id, FormCollection collection)
        {
            
                Job job = db.Jobs.Find(id);
                db.Jobs.Remove(job);
                db.SaveChanges();
                return RedirectToAction("MyJobs");
            
        }
    }
}
