using FreelancerMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FreelancerMarketplace.Controllers
{ 
    public class HomeController : Controller
    {
        private DB66Entities db = new DB66Entities();
        public ActionResult Index()
        {
            var categories = db.Categories;
            foreach (Category category in categories)
            {
                category.countOfJobs = db.Jobs.Where(x => x.CategoryID == category.CategoryId).Count();
            }
            return View(categories);
        }
        
        public ActionResult CategoryJobs(int? id)
        {
            var jobs = db.Jobs.Where(x => x.CategoryID == id);
            return View(jobs);
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

        [AllowAnonymous]
        public ActionResult Search(string SearchString)
        {
            var jobsTitle = db.Jobs.Where(x => x.Title.StartsWith(SearchString));
            var jobsSkill = db.Jobs.Where(x => x.Skill.SkillName.StartsWith(SearchString));
            var jobCategory = db.Jobs.Where(x => x.Category.CategoryName.StartsWith(SearchString));

            JobSearchViewModel jobview = new JobSearchViewModel();
            jobview.JobTitle = jobsTitle;
            jobview.JobSkill = jobsSkill;
            jobview.JobCategory = jobCategory;
            return View(jobview);
        }
    }
}