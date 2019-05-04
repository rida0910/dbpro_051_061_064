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
                    job.countOfBids = db.Bids.Where(x => x.JobID == job.JobId && x.Active == 1 && x.Accepted == false).Count();
                    job.countOfAcceptedBids = db.Bids.Where(x => x.JobID == job.JobId && x.Active == 1 && x.Accepted == true).Count();
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

            var bids = db.Bids.Where(x => x.JobID == job.JobId && x.Active == 1 && x.Accepted == false);
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

        public ActionResult ManageFreelancers(int? id)
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

            var bids = db.Bids.Where(x => x.JobID == job.JobId && x.Active == 1 && x.Accepted == true);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            if (bid == null)
            {
                return HttpNotFound();
            }
            Person p = db.People.FirstOrDefault(x => x.PersonId == bid.FreelancerID);
            ViewBag.Name = p.FirstName + " " + p.LastName;

            return View(bid);
        }

        [HttpPost]
        public ActionResult AwardProject(int id)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            bid.Accepted = true;
            db.SaveChanges();
            return RedirectToAction("MyJobs");
        }

        public ActionResult UnAssignProject(int? id)
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
            Person p = db.People.FirstOrDefault(x => x.PersonId == bid.FreelancerID);
            ViewBag.Name = p.FirstName + " " + p.LastName;

            return View(bid);
        }

        [HttpPost]
        public ActionResult UnAssignProject(int id)
        {
            Bid bid = db.Bids.FirstOrDefault(x => x.BidId == id);
            bid.Accepted = false;
            bid.Active = 0;
            db.SaveChanges();
            return RedirectToAction("MyJobs");
        }

        public ActionResult SendMessage(int? id)
        {
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage([Bind(Include = "Text")] Message msg, int id, HttpPostedFileBase AttachmentFile)
        {
            string id1 = User.Identity.GetUserId();
            msg.EmployerId = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            msg.FreelancerId = db.Freelancers.FirstOrDefault(x => x.FreelancerId == id).FreelancerId;
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

            msg.SentBy = msg.EmployerId;
            db.Messages.Add(msg);
            db.SaveChanges();
            return RedirectToAction("MyJobs");
        }

        public ActionResult Messages()
        {
            string id = User.Identity.GetUserId();
            int Eid = db.People.FirstOrDefault(x => x.User_AccountID == id).PersonId;
            var messages = db.Messages.Where(x => x.EmployerId == Eid);
            List<Person> list = new List<Person>();
            foreach (Message msg in messages)
            {
                Person person = new Person();
                person = db.People.FirstOrDefault(x => x.PersonId == msg.FreelancerId);
                if (!list.Contains(person))
                {
                    list.Add(person);
                }

            }

            return View(list);
        }

        public ActionResult DisplayMessages(int? id)
        {
            string id1 = User.Identity.GetUserId();
            int Eid = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            var messages = db.Messages.Where(x => x.EmployerId == Eid && x.FreelancerId == id);
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            ViewBag.employer = Eid;
            ViewBag.freePicture = p.ProfilePicture;
            ViewBag.EmpPicture = db.People.FirstOrDefault(x => x.User_AccountID == id1).ProfilePicture;
            return View(messages);
        }

        [HttpPost]
        public ActionResult DisplayMessages(int id, string msgText, HttpPostedFileBase AttachmentFile)
        {
            string id1 = User.Identity.GetUserId();
            int Eid = db.People.FirstOrDefault(x => x.User_AccountID == id1).PersonId;
            Message message = new Message();
            message.Text = msgText;
            message.EmployerId = Eid;
            message.Time = DateTime.Now;
            message.FreelancerId = id;
            message.SentBy = message.EmployerId;

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



            var messages = db.Messages.Where(x => x.EmployerId == Eid && x.FreelancerId == id);
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            ViewBag.Name = p.FirstName + " " + p.LastName;
            ViewBag.employer = Eid;
            ViewBag.freePicture = p.ProfilePicture;
            ViewBag.EmpPicture = db.People.FirstOrDefault(x => x.User_AccountID == id1).ProfilePicture;
            return View(messages);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.FirstOrDefault(x => x.JobId == id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttachmentID = new SelectList(db.Attachments, "AttachmentId", "Path", job.AttachmentID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryName", job.CategoryID);
            ViewBag.SkillID = new SelectList(db.Skills, "SkillId", "SkillName", job.SkillID);
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value", job.JobType);
            return View(job);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Description,MinPayment,MaxPayment,AttachmentFile,CategoryId,Deadline,JobType")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyJobs");
            }
            ViewBag.AttachmentID = new SelectList(db.Attachments, "AttachmentId", "Path", job.AttachmentID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryName", job.CategoryID);
            ViewBag.SkillID = new SelectList(db.Skills, "SkillId", "SkillName", job.SkillID);
            ViewBag.JobType = new SelectList(db.Lookups, "Id", "value", job.JobType);
            return View(job);
        }
        public ActionResult AddInformation()
        {
            string id = User.Identity.GetUserId();
            if (db.People.Any(x => x.User_AccountID == id))
            {
                RedirectToAction("MyJobs");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInformation(string FirstName, string LastName, string Gender, string Nationality, string Address, string CompanyName, HttpPostedFileBase ImageFile)
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

                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                person.ProfilePicture = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                ImageFile.SaveAs(fileName);

                db.People.Add(person);
                db.SaveChanges();


                Company company = new Company();
                int cId;
                if (!db.Companies.Any(x => x.CompanyName == CompanyName))
                {
                    company.CompanyName = CompanyName;
                    db.Companies.Add(company);
                    db.SaveChanges();
                    cId = company.CompanyId;
                }
                else
                {
                    cId = db.Companies.FirstOrDefault(x => x.CompanyName == CompanyName).CompanyId;
                }

                Employer employer = new Employer();
                employer.EmployerId = person.PersonId;
                employer.CompanyId = cId;

                db.Employers.Add(employer);
                db.SaveChanges();

            }
            return RedirectToAction("MyJobs");
        }

        // GET: Employer/DeleteJob/5
        public ActionResult DeleteJob(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.FirstOrDefault(x => x.JobId == id);
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

            Job job = db.Jobs.FirstOrDefault(x => x.JobId == id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("MyJobs");

        }

        public FileResult Download(int? id)
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

        public ActionResult FindFreelancer()
        {
            var freelancers = db.Freelancers;
            List<FreelancerViewModel> list = new List<FreelancerViewModel>();

            foreach (Freelancer freelancer in freelancers)
            {
                Person p = db.People.FirstOrDefault(x => x.PersonId == freelancer.FreelancerId);
                if (p.Approved == true)
                {
                    FreelancerViewModel freelancerViewModel = new FreelancerViewModel();
                    freelancerViewModel.Freelancer = freelancer;
                    freelancerViewModel.Person = p;
                    AspNetUser aspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == p.User_AccountID);
                    freelancerViewModel.AspNetUser = aspNetUser;
                    var Fskills = db.FreelancerSkills.Where(s => s.FreelancerId == freelancer.FreelancerId);
                    freelancerViewModel.FreelancerSkill = Fskills;
                    list.Add(freelancerViewModel);
                }
            }

            return View(list);
        }

        public ActionResult FreelancerProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            if (p == null)
            {
                return HttpNotFound();
            }
            AspNetUser aspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == p.User_AccountID);
            Freelancer freelancer = db.Freelancers.FirstOrDefault(f => f.FreelancerId == p.PersonId);
            var Fskills = db.FreelancerSkills.Where(s => s.FreelancerId == freelancer.FreelancerId);

            FreelancerViewModel freelancerViewModel = new FreelancerViewModel();
            freelancerViewModel.Person = p;
            freelancerViewModel.AspNetUser = aspNetUser;
            freelancerViewModel.Freelancer = freelancer;
            freelancerViewModel.FreelancerSkill = Fskills;

            return View(freelancerViewModel);
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
    }
}
