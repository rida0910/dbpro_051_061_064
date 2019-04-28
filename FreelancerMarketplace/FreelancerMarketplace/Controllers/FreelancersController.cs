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
    [Authorize(Roles = "Freelancer")]
    public class FreelancersController : Controller
    {
        private DB66Entities db = new DB66Entities();

        

        public ActionResult Managejobs()
        {
            var bids = db.Bids;
            var userid = User.Identity.GetUserId();
            
            List<JobViewModel> list = new List<JobViewModel>();
            foreach (Bid bid in bids)
            {
                JobViewModel jobView = new JobViewModel();
                if (bid.FreelancerID == db.People.FirstOrDefault(x => x.User_AccountID == userid).PersonId && bid.Accepted == true && bid.Active == 1)
                {
                    Job job = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID);
                    jobView.Bid = bid;
                    jobView.Job = job;
                    list.Add(jobView);
                }
            }
            return View(list);
        }

        

        // GET: Freelancers/Create
        public ActionResult AddInfo()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups.Where(x => x.category == "EXPERIENCE"), "Id", "value");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View();
        }

        // POST: Freelancers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInfo([Bind(Include = "ImageFile,ProfessionalTitle,ProfessionalOverview,CategoryId,ExperienceInCategory,JobType")] Freelancer freelancer, 
            string FirstName, string LastName, string Gender, string Nationality, string Address, string Skills)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person();
                person.FirstName = FirstName;
                person.LastName = LastName;
                person.Gender = db.Lookups.FirstOrDefault(x => x.value.Equals(Gender)).Id;
                person.Nationality = Nationality;
                person.Address = Address;
                person.AccountType = "Freelancer";
                person.User_AccountID = User.Identity.GetUserId();
                person.Approved = false;

                string fileName = Path.GetFileNameWithoutExtension(freelancer.ImageFile.FileName);
                string extension = Path.GetExtension(freelancer.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                freelancer.ProfilePicture = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                freelancer.ImageFile.SaveAs(fileName);

                person.ProfilePicture = freelancer.ProfilePicture;
                db.People.Add(person);
                db.SaveChanges();

                freelancer.FreelancerId = person.PersonId;
                db.Freelancers.Add(freelancer);
                db.SaveChanges();

                
                
                
                var SkillsList = Skills.Split(',');
                foreach (string s in SkillsList)
                {
                    Skill skill = new Skill();
                    if (!db.Skills.Any(x => x.SkillName == s))
                    {
                        
                        skill.SkillName = s;
                        db.Skills.Add(skill);
                        db.SaveChanges();

                        if (!db.FreelancerSkills.Any(x => x.Skill.SkillName == s))
                        {
                            FreelancerSkill freelancerSkill = new FreelancerSkill();
                            freelancerSkill.Skill = skill;
                            freelancerSkill.SkillId = skill.SkillId;
                            freelancerSkill.FreelancerId = freelancer.FreelancerId;
                            db.FreelancerSkills.Add(freelancerSkill);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (!db.FreelancerSkills.Any(x => x.Skill.SkillName == s))
                        {
                            FreelancerSkill freelancerSkill = new FreelancerSkill();
                            freelancerSkill.SkillId = db.Skills.FirstOrDefault(x => x.SkillName.Equals(s)).SkillId;
                            freelancerSkill.FreelancerId = freelancer.FreelancerId;
                            db.FreelancerSkills.Add(freelancerSkill);
                            db.SaveChanges();
                        }
                    }
                }

                ModelState.Clear();
                return RedirectToAction("Dashboard");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups.Where(x => x.category == "EXPERIENCE"), "Id", "value");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View(freelancer);
        }

        public ActionResult AvailableJobs()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            List<Job> list = new List<Job>();
            var jobs = db.Jobs;
            var userid = User.Identity.GetUserId();
            int freeId = db.People.FirstOrDefault(p => p.User_AccountID == userid && p.AccountType.Equals("Freelancer")).PersonId;
            foreach (Job job in jobs)
            {
                int jobId = job.JobId;
                if (!db.Bids.Any(b => b.JobID.Equals(jobId) && b.FreelancerID.Equals(freeId)))
                {
                    list.Add(job);
                }
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult AvailableJobs(Job job, string Location, string PaymentRange)
        {
            int category = job.CategoryID;
            string catname = db.Categories.FirstOrDefault(x => x.CategoryId == category).CategoryName;
            string loc = Location;
            string range = PaymentRange;



            return RedirectToAction("AvailableJobs");
        }

        public ActionResult Bid(int? id)
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
            int empid = job.EmployerID;
            Employer emp = db.Employers.FirstOrDefault(e => e.EmployerId.Equals(empid));
            Person person = db.People.FirstOrDefault(p => p.PersonId.Equals(empid));
            ViewBag.Nationality = person.Nationality;
            ViewBag.Name = person.FirstName + " " + person.LastName;
            

            
            var jobskils = db.JobSkills;
            List<string> list = new List<string>();
            foreach (JobSkill js in jobskils)
            {
                if (js.JobId == id)
                {
                    Skill skill = db.Skills.FirstOrDefault(x => x.SkillId == js.SkillId);
                    list.Add(skill.SkillName);
                }
            }
            ViewBag.OtherSkills = list;
            Bid bid = new Bid();
            ViewBag.DeliveryTime = bid.DeliveryTime;

            JobViewModel jobview = new JobViewModel();
            jobview.Job = job;

            return View(jobview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(int id, string BidAmount, JobViewModel jobView)
        {
            int bidamount = int.Parse(BidAmount);
            DateTime? time = jobView.Bid.DeliveryTime;

            Bid bid = new Bid();
            bid.Active = 1;
            bid.Accepted = false;
            var userid = User.Identity.GetUserId();
            int freeId = db.People.FirstOrDefault(p => p.User_AccountID == userid && p.AccountType.Equals("Freelancer")).PersonId;
            bid.FreelancerID = freeId;
            bid.JobID = id;
            bid.BidTime = DateTime.Now;
            bid.PaymentAmount = bidamount;
            bid.DeliveryTime = time;
            db.Bids.Add(bid);
            db.SaveChanges();

            return RedirectToAction("AvailableJobs");
        }

        public FileResult Download (int id)
        {
            var attachment = db.Attachments.FirstOrDefault(a => a.AttachmentId == id);
            return File(attachment.Path, GetMimeType(Path.GetFileName(attachment.Path)), "ProjectFile" + Path.GetExtension(attachment.Path));
        }


        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }


        public ActionResult MyActiveBids()
        {
            var userid = User.Identity.GetUserId();
            int freeId = db.People.FirstOrDefault(p => p.User_AccountID == userid && p.AccountType.Equals("Freelancer")).PersonId;
            var bids = db.Bids.Where(x => x.FreelancerID == freeId && x.Active.Equals(1) && x.Accepted == false);
  
            List<JobViewModel> list = new List<JobViewModel>();

            foreach (Bid bid in bids)
            {
                Job job = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID);
                JobViewModel jobView = new JobViewModel();
                jobView.Bid = bid;
                jobView.Job = job;
                list.Add(jobView);
            }
            

            return View(list);
        }

        public ActionResult CancelBid(int? id)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            db.Bids.Remove(bid);
            db.SaveChanges();
            return RedirectToAction("MyActiveBids");
        }

        
        [HttpPost]
        public ActionResult EditBid(int? id, JobViewModel jobView)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            db.SaveChanges();
            return RedirectToAction("MyActiveBids");

        }
    }
}
