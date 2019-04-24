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

        // GET: Employer/Create
        public ActionResult AddInfo()
        {
            return View();

        } 
        public ActionResult PostAJob()
        {
            return View();
        }
        public ActionResult FreelancerProfileSeenByEmployer()
        {
            return View();
        }
        public ActionResult Managejobs()
        {
            return View();
        }
        public ActionResult ManageCandidateDashboard()
        {
            return View();
        }



        public ActionResult Messages()
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employer/Edit/5
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

        // POST: Employer/DeleteJob/5
        [HttpPost, ActionName("DeleteJob")]
        [ValidateAntiForgeryToken]
        public ActionResult JobDeleted(int id, FormCollection collection)
        {
            
                // TODO: Add delete logic here

                Job job = db.Jobs.Find(id);
                db.Jobs.Remove(job);
                db.SaveChanges();
                return RedirectToAction("MyJobs");
            
        }
    }
}
