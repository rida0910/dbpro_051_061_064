using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreelancerMarketplace.Models;
using System.IO;
namespace FreelancerMarketplace.Controllers
{
    public class ReportsController : Controller
    {
   
        public ActionResult Index()
        {
            return FreelancerList();
        }
        public ActionResult FreelancerList()
        {
            FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();
            //CrMVCApp.Models.Customer c;
            var c = (from b in db.ListOfFreelancers select b).ToList();

            FreelancersList rpt = new FreelancersList();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult JobByEmployers()
        {
            return EmployersPostedJobs();
        }
        public ActionResult EmployersPostedJobs()
        {
            FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();

            var c = (from b in db.JobsPostedByEmployer select b).ToList();

            PostedJobsOfEmployer rpt = new PostedJobsOfEmployer();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult EmployersInactiveProjects()
        {
            return EmployersPostedJobs();
        }

        public ActionResult EmpInactiveProject()
        {
            FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();

            var c = (from b in db.InactiveProjectsOfEmployers select b).ToList();

            InacttiveProjects rpt = new InacttiveProjects();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult FreelancerPrevioualyMadeProjects()
        {
            return freelancerPrevProject();
        }

        public ActionResult freelancerPrevProject()
        {
            FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();

            var c = (from b in db.JobsDoneByFreelancers select b).ToList();

            InacttiveProjects rpt = new InacttiveProjects();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult FreelancerWithSameEmployerDifPro()
        {
            return freelancerEmployerOndifProjects();
        }
        public ActionResult freelancerEmployerOndifProjects()
        {
            FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();

            var c = (from b in db.SameEmployersDifferentProjects select b).ToList();

            FreelancerWithSameEmployer rpt = new FreelancerWithSameEmployer();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }


        // GET: Reports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
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

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
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

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
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
