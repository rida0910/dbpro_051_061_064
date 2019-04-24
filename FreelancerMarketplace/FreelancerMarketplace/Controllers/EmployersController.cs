using FreelancerMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerMarketplace.Controllers
{
    public class EmployersController : Controller
    {
        private DB66Entities db = new DB66Entities();
        // GET: Employers
        public ActionResult POstAJOb()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View();
        }



    }
}