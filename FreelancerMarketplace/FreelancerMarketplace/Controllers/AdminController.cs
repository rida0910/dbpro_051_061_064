using FreelancerMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerMarketplace.Controllers
{
    public class AdminController : Controller
    {
        private DB66Entities db = new DB66Entities();
        
        public ActionResult ViewFreelancers()
        {
            var freelancers = db.Freelancers;
            List<FreelancerViewModel> list = new List<FreelancerViewModel>();
           
            foreach(Freelancer freelancer in freelancers)
            {
                FreelancerViewModel freelancerViewModel = new FreelancerViewModel();
                freelancerViewModel.Freelancer = freelancer;
                Person p = db.People.FirstOrDefault(x => x.PersonId == freelancer.FreelancerId);
                freelancerViewModel.Person = p;
                AspNetUser aspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == p.User_AccountID);
                freelancerViewModel.AspNetUser = aspNetUser;
                var Fskills = db.FreelancerSkills.Where(s => s.FreelancerId == freelancer.FreelancerId);
                freelancerViewModel.FreelancerSkill = Fskills;
                list.Add(freelancerViewModel);
            }

            return View(list);
        }

        public ActionResult ViewProfile(int? id)
        {
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
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
        
        public ActionResult ViewEmployers(int id)
        {
            return View();
        }

        public ActionResult CancelRegistration(int id)
        {
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            p.Approved = true;
            db.SaveChanges();
            return RedirectToAction("ViewFreelancers");
        }
       
    }
}
