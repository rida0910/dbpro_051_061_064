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
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private DB66Entities db = new DB66Entities();
        

        public ActionResult PostAJob()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAJob([Bind(Include = "Title,Description,MinPayment,MaxPayment,AttachmentFile,CategoryId,Deadline,JobType")] Job job,
            string MainSkill, string OtherSkills)
        {
            if (ModelState.IsValid)
            {
                Attachment attachment = new Attachment();
                string fileName = Path.GetFileNameWithoutExtension(job.AttachmentFile.FileName);
                string extension = Path.GetExtension(job.AttachmentFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                attachment.Path = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                job.AttachmentFile.SaveAs(fileName);

                db.Attachments.Add(attachment);
                db.SaveChanges();

                job.AttachmentID = attachment.AttachmentId;
                Skill skill = new Skill();
                if (!db.Skills.Any(x => x.SkillName == MainSkill))
                {

                    skill.SkillName = MainSkill;
                    db.Skills.Add(skill);
                    db.SaveChanges();

                    job.SkillID = skill.SkillId;
                }
                else
                {
                    job.SkillID = db.Skills.FirstOrDefault(x => x.SkillName.Equals(MainSkill)).SkillId;
                }
                var id = User.Identity.GetUserId();
                job.EmployerID = db.People.FirstOrDefault(p => p.User_AccountID.Equals(id)).PersonId;
                job.TimePosted = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();

                var SkillsList = OtherSkills.Split(',');
                foreach (string s in SkillsList)
                {
                    Skill skill1 = new Skill();
                    if (!db.Skills.Any(x => x.SkillName == s))
                    {

                        skill1.SkillName = s;
                        db.Skills.Add(skill1);
                        db.SaveChanges();

                        if (!db.JobSkills.Any(x => x.Skill.SkillName == s))
                        {
                            JobSkill jobSkill = new JobSkill();
                            jobSkill.SkillId = skill1.SkillId;
                            jobSkill.JobId = job.JobId;
                            db.JobSkills.Add(jobSkill);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (!db.FreelancerSkills.Any(x => x.Skill.SkillName == s))
                        {
                            JobSkill jobSkill = new JobSkill();
                            jobSkill.SkillId = db.Skills.FirstOrDefault(x => x.SkillName.Equals(s)).SkillId;
                            jobSkill.JobId = job.JobId;
                            db.JobSkills.Add(jobSkill);
                            db.SaveChanges();
                        }
                    }
                }

                ModelState.Clear();
                return RedirectToAction("MyJobs");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View(job);
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
                    job.countOfBids = db.Bids.Where(x => x.JobID == job.JobId).Count();
                    MyJobs.Add(job);
                }
            }
            return View(MyJobs);
        }

        public ActionResult ManageBidders(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Job job = db.Jobs.FirstOrDefault(j => j.JobId == id);
            if (job == null)
            {
                return HttpNotFound();
            }

            var bids = db.Bids.Where(x => x.JobID == job.JobId && x.Active == 1);
            BiddersViewModel biddersViewModel = new BiddersViewModel();
            biddersViewModel.Bids = bids;
            biddersViewModel.Job = job;
            List<Person> people = new List<Person>();
            List<AspNetUser> users = new List<AspNetUser>();
            foreach (Bid bid in bids)
            {
                Person person = db.People.FirstOrDefault(x => x.PersonId == bid.FreelancerID);
                people.Add(person);
                AspNetUser user = db.AspNetUsers.FirstOrDefault(x => x.Id == person.User_AccountID);
                users.Add(user);
            }

            biddersViewModel.People = people;
            biddersViewModel.AspNetUsers = users;

            return View(biddersViewModel);
        }

        public ActionResult AwardProject(int? id)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            bid.Accepted = true;
            db.SaveChanges();
            return RedirectToAction("MyJobs");
        }
        
        public ActionResult Messages()
        {
            return View();
        }
   
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
