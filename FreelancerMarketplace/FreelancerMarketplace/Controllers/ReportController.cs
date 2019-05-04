using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CrMVCApp;
using System.Web.Mvc;

namespace FreelancerMarketplace.Controllers
{

    public class ReportController : Controller
    {
        FreelancerMarketplace.Models.DB66Entities db = new FreelancerMarketplace.Models.DB66Entities();
       
        // GET: Report
        public ActionResult Employers()
        {
            return ShowEmployersList();
        }
        public ActionResult ShowEmployersList()
        {

            //CrMVCApp.Models.Customer c;
            var c = (from b in db.EmployersLists select b).ToList();

            Employers rpt = new Employers();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult ActiveProjects()
        {
            return ShowEmployersActiveProjects();
        }
        public ActionResult ShowEmployersActiveProjects()
        {

            //CrMVCApp.Models.Customer c;
            var c = (from b in db.EmployersActiveProjects select b).ToList();

            EmpActiveJobs rpt = new EmpActiveJobs();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult UnAccepteJobs()
        {
            return ShowEmployersUnacceptedProjects();
        }
        public ActionResult ShowEmployersList()
        {

            //CrMVCApp.Models.Customer c;
            var c = (from b in db.EmployersLists select b).ToList();

            Employers rpt = new Employers();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}