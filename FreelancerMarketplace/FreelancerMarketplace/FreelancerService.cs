//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreelancerMarketplace
{
    using System;
    using System.Collections.Generic;
    
    public partial class FreelancerService
    {
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public int SkillId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Fee { get; set; }
        public string PicturePath { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
