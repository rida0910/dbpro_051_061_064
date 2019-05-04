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

            var c = (from b in db.JobsPostedByEmployers select b).ToList();

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

            FreelancerPreviousProjects rpt = new FreelancerPreviousProjects();
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


  
       
        }
    }

