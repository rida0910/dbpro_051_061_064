﻿using System;
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
        public ActionResult ManageBidders(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            Bid Bid = db.Bids.FirstOrDefault(b => b.BidId == id);
            List<Bid> AllBids = new List<Bid>();
            foreach (Bid bid in db.Bids)
            {
                if (bid.JobID == job.JobId)
                {
                    AllBids.Add(bid);
                }

            }
            ViewBag.Bids = AllBids;

            List<Freelancer> freelancers = new List<Freelancer>();
            foreach (Freelancer free in db.Freelancers)
            {
                if (free.FreelancerId == Bid.FreelancerID)
                {
                    freelancers.Add(free);
                }
            }
            ViewBag.Freelancers = freelancers;
            int freelancerid = Bid.FreelancerID;
            Freelancer freelancer = db.Freelancers.FirstOrDefault(f => f.FreelancerId.Equals(freelancerid));
            Person person = db.People.FirstOrDefault(p => p.PersonId.Equals(freelancerid));
            int accountID = AspNetUser.Id;
            AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Id.Equals(accountID));

            ViewBag.Name = person.FirstName + " " + person.LastName;
            ViewBag.Email = user.Email;
            ViewBag.DeliveryTime = Bid.DeliveryTime;
            ViewBag.Payment = Bid.PaymentAmount;

            BiddersViewModel biddersview = new BiddersViewModel();
            biddersview.Bid = Bid;

            return View(biddersview);
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

            List<Job> MyJobs = new List<Job>();
            foreach (Job job in db.Jobs)
            {
                if (job.EmployerID == empId)
                {
                    MyJobs.Add(job);
                }
            }
            return View(MyJobs);
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
        public ActionResult AddInformation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInformation([Bind(Include = "ImageFile,CompanyName")] Employer employer,
           string FirstName, string LastName, string Gender, string Nationality, string Address, string company)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person();
                person.FirstName = FirstName;
                person.LastName = LastName;
                person.Gender = db.Lookups.FirstOrDefault(x => x.value.Equals(Gender)).Id;
                person.Nationality = Nationality;
                person.Address = Address;
                person.AccountType = "Employer";
                person.User_AccountID = User.Identity.GetUserId();
                person.Approved = false;

                string fileName = Path.GetFileNameWithoutExtension(employer.ImageFile.FileName);
                string extension = Path.GetExtension(employer.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                employer.ProfilePicture = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                employer.ImageFile.SaveAs(fileName);

                person.ProfilePicture = employer.ProfilePicture;
                db.People.Add(person);
                db.SaveChanges();

                employer.EmployerId = person.PersonId;
                db.Employers.Add(employer);
                db.SaveChanges();

            }
            return View(employer);
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
