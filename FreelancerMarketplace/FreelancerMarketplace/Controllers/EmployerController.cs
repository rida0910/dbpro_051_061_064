using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerMarketplace.Controllers
{
    public class EmployerController : Controller
    {
        // GET: Employer
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Employer/Details/5
        public ActionResult ManageBidders(int id)
        {
            return View();
        }

        // GET: Employer/Create
        public ActionResult AddInfo()
        {
            return View();

        } 
        public ActionResult PostAJob()
        {
            return View();
        }
        public ActionResult FreelancerProfileSeenByEmployer()
        {
            return View();
        }
        public ActionResult ManagJobs()
        {
            return View();
        }
        public ActionResult ManageCandidateDashboard()
        {
            return View();
        }



        public ActionResult Messages()
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
