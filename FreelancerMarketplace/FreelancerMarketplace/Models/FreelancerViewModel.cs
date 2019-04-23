using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerMarketplace.Models
{
    public class FreelancerViewModel
    {
        public int freelancerID { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        
        public string Category { get; set; }
        public string ExperienceInCategory { get; set; }
        public string ProfessionalTitle { get; set; }
        public string ProfessionalOverview { get; set; }
        public string JobType { get; set; }

        public List<string> skills { get; set; } 

        public HttpPostedFileBase ImageFile { get; set; }

    }
}