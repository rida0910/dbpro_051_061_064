using FreelancerMarketplace.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POstAJOb([Bind(Include = "Title,Description,MinPayment,MaxPayment,AttachmentFile,CategoryId,Deadline,JobType")] Job job,
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
                return RedirectToAction("Dashboard");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.JobType = new SelectList(db.Lookups.Where(x => x.category.Equals("JOBTYPE")), "Id", "value");
            return View(job);
        }



    }
}