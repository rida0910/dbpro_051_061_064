using FreelancerMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
    }
}