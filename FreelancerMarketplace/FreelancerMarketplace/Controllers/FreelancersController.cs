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
    [Authorize(Roles = "Freelancer, Admin")]
    public class FreelancersController : Controller
    {
        private DB66Entities db = new DB66Entities();

        

        public ActionResult Managejobs()
        {
            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

            var bids = db.Bids.Where(x => x.Accepted == true && x.Active == 1);
            var userid = User.Identity.GetUserId();
            
            List<JobViewModel> list = new List<JobViewModel>();
            foreach (Bid bid in bids)
            {
                JobViewModel jobView = new JobViewModel();
                int pid = db.People.FirstOrDefault(x => x.User_AccountID == userid).PersonId;
                if (bid.FreelancerID == pid)
                {
                    Job job = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID);
                    jobView.Bid = bid;
                    jobView.Job = job;
                    list.Add(jobView);
                }
            }
            return View(list);
        }

        

        
        public ActionResult AddInfo()
        {
            string id = User.Identity.GetUserId();
            //bool result = db.People.Any(x => x.User_AccountID == id);
            if (db.People.Any(x => x.User_AccountID == id))
            {
                return RedirectToAction("AvailableJobs");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.ExperienceInCategory = new SelectList(db.Lookups.Where(x => x.category == "EXPERIENCE"), "Id", "value");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View();
        }

        
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
                return RedirectToAction("AvailableJobs");
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
            var payment = range.Split(',');

            int Min = int.Parse(payment[0]);
            int Max = int.Parse(payment[1]);


            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            List<Job> list = new List<Job>();
            var jobs = db.Jobs.Where(x => x.Category.CategoryName == catname && x.MinPayment >= Min && x.MaxPayment <= Max);
            var userid = User.Identity.GetUserId();
            int freeId = db.People.FirstOrDefault(p => p.User_AccountID == userid && p.AccountType.Equals("Freelancer")).PersonId;
            foreach (Job job1 in jobs)
            {
                int jobId = job1.JobId;
                if (!db.Bids.Any(b => b.JobID.Equals(jobId) && b.FreelancerID.Equals(freeId)))
                {
                    
                    list.Add(job1);
                }
            }
            return View(list);
        }

        public ActionResult Bid(int? id)
        {

            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

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
            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

            var userid = User.Identity.GetUserId();
            int freeId = db.People.FirstOrDefault(p => p.User_AccountID == userid && p.AccountType.Equals("Freelancer")).PersonId;
            var bids = db.Bids.Where(x => x.FreelancerID == freeId && x.Active.Equals(1) && x.Accepted == false);
  
            List<Bid> list = new List<Bid>();
            List<string> joblist = new List<string>();
            foreach (Bid bid in bids)
            {
                Job job = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID);
                //JobViewModel jobView = new JobViewModel();
                //jobView.Bid = bid;
                //jobView.Job = job;
                list.Add(bid);
                joblist.Add(job.Title);
                
            }

            ViewBag.JobTitle = joblist;
            return View(list);
        }

        public ActionResult CancelBid(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobTitle = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID).Title;
            return View(bid);
        }

        [HttpPost]
        public ActionResult CancelBid(int id)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            db.Bids.Remove(bid);
            db.SaveChanges();
            return RedirectToAction("MyActiveBids");
        }

        public ActionResult EditBid(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            ViewBag.Min = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID).MinPayment;
            ViewBag.Max = db.Jobs.FirstOrDefault(x => x.JobId == bid.JobID).MaxPayment;
            return View(bid);
        }

        [HttpPost]
        public ActionResult EditBid([Bind(Include = "DeliveryTime")]Bid bid, string BidAmount)
        {
            if (ModelState.IsValid)
            {
                bid.PaymentAmount = int.Parse(BidAmount);
                db.Entry(bid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyActiveBids");
                
            }
            ModelState.AddModelError("", "Unable to save changes. Try again!");
            return View(bid);
        }

        public ActionResult JobDetails(int? id)
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

        public ActionResult ViewBids(int? id)
        {
            var bids = db.Bids.Where(x => x.JobID == id);
            BiddersViewModel bidders = new BiddersViewModel();
            bidders.Bids = bids;
            List<Person> people = new List<Person>();
            foreach (Bid bid in bids)
            {
                Person p = db.People.FirstOrDefault(x => x.PersonId == bid.FreelancerID);
                people.Add(p);

            }
            bidders.People = people;
            bidders.Job = db.Jobs.FirstOrDefault(x => x.JobId == id);
            return View(bidders);
        }

        public ActionResult Messages()
        {
            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

            string id = User.Identity.GetUserId();
            int Fid = db.People.FirstOrDefault(x => x.User_AccountID == id).PersonId;
            var messages = db.Messages.Where(x => x.FreelancerId == Fid);
            List<Person> list = new List<Person>();
            foreach (Message msg in messages)
            {
                Person person = new Person();
                person = db.People.FirstOrDefault(x => x.PersonId == msg.EmployerId);
                if (!list.Contains(person))
                {
                    list.Add(person);
                }

            }

            return View(list);
        }

        public ActionResult DisplayMessages(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

            string id1 = User.Identity.GetUserId();
            int Fid = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            var messages = db.Messages.Where(x => x.FreelancerId == Fid && x.EmployerId == id);
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            ViewBag.Freelancer = Fid;
            ViewBag.EmployerPicture = p.ProfilePicture;
            ViewBag.FreePicture = db.People.FirstOrDefault(x => x.User_AccountID == id1).ProfilePicture;
            return View(messages);
        }

        [HttpPost]
        public ActionResult DisplayMessages(int id, string msgText, HttpPostedFileBase AttachmentFile)
        {
            string id1 = User.Identity.GetUserId();
            int Fid = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            Message message = new Message();
            message.Text = msgText;
            message.FreelancerId = Fid;
            message.Time = DateTime.Now;
            message.EmployerId = id;
            message.SentBy = message.FreelancerId;

            if (AttachmentFile != null)
            {
                Attachment attachment = new Attachment();
                string fileName = Path.GetFileNameWithoutExtension(AttachmentFile.FileName);
                string extension = Path.GetExtension(AttachmentFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                attachment.Path = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                AttachmentFile.SaveAs(fileName);

                db.Attachments.Add(attachment);
                db.SaveChanges();

                message.AttachmentId = attachment.AttachmentId;
            }
            else
            {
                message.AttachmentId = null;
            }
            db.Messages.Add(message);
            db.SaveChanges();



            var messages = db.Messages.Where(x => x.FreelancerId == Fid && x.EmployerId == id);
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            ViewBag.Freelancer = Fid;
            ViewBag.EmployerPicture = p.ProfilePicture;
            ViewBag.FreePicture = db.People.FirstOrDefault(x => x.User_AccountID == id1).ProfilePicture;
            return View(messages);
        }

        public ActionResult SendMessage(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == userId && x.Approved == false))
            {
                ViewBag.Message = "Your Registration Request is pending...";

                return View("Info");
            }

            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage([Bind(Include = "Text")] Message msg, int id, HttpPostedFileBase AttachmentFile)
        {
            string id1 = User.Identity.GetUserId();
            msg.FreelancerId = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            msg.EmployerId = id;
            msg.Time = DateTime.Now;

            if (AttachmentFile != null)
            {
                Attachment attachment = new Attachment();
                string fileName = Path.GetFileNameWithoutExtension(AttachmentFile.FileName);
                string extension = Path.GetExtension(AttachmentFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                attachment.Path = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                AttachmentFile.SaveAs(fileName);

                db.Attachments.Add(attachment);
                db.SaveChanges();

                msg.AttachmentId = attachment.AttachmentId;
            }

            msg.SentBy = msg.FreelancerId;
            db.Messages.Add(msg);
            db.SaveChanges();
            return RedirectToAction("ManageJobs");
        }
    }
}
