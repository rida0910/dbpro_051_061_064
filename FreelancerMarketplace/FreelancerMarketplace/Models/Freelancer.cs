//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreelancerMarketplace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Freelancer
    {
        public int FreelancerId { get; set; }
        public int CategoryId { get; set; }
        public int ExperienceInCategory { get; set; }
        public string ProfessionalTitle { get; set; }
        public string ProfessionalOverview { get; set; }
        public int JobType { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Lookup Lookup { get; set; }
        public virtual Lookup Lookup1 { get; set; }
    }
}
