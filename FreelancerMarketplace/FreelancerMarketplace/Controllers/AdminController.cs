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

        public ActionResult ViewFreelancerProfile(int? id)
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
        
        public ActionResult ViewEmployers()
        {
            var employers = db.Employers;
            List<EmployerViewModel> list = new List<EmployerViewModel>();

            foreach (Employer employer in employers)
            {
                Person p = db.People.FirstOrDefault(x => x.PersonId == employer.EmployerId);
                EmployerViewModel employerViewModel = new EmployerViewModel();
                employerViewModel.Employer = employer;
                employerViewModel.Person = p;
                AspNetUser aspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == p.User_AccountID);
                employerViewModel.AspNetUser = aspNetUser;
                Company company = db.Companies.FirstOrDefault(x => x.CompanyId == employer.CompanyId);
                employerViewModel.Company = company;
                list.Add(employerViewModel);

            }
            return View(list);
        }

        public ActionResult CancelRegistration(int id)
        {
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            p.Approved = false;
            db.SaveChanges();
            if (p.AccountType == "Freelancer")
            {
                return RedirectToAction("ViewFreelancers");
            }
            else if (p.AccountType == "Employer")
            {
                return RedirectToAction("ViewEmployers");
            }
            return RedirectToAction("ApprovalRequests");
        }
        
        public ActionResult ApprovalRequests()
        {
            var people = db.People.Where(x => x.Approved == false && x.AccountType == "Freelancer");
            return View(people.ToList());
        }

        public ActionResult ApproveRegistration(int id)
        {
            Person p = db.People.FirstOrDefault(x => x.PersonId == id);
            p.Approved = true;
            db.SaveChanges();
            
            return RedirectToAction("ApprovalRequests");
        }
    }
}
