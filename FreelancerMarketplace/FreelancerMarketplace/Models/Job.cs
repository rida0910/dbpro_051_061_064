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
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> AttachmentID { get; set; }
        public int SkillID { get; set; }
        public int EmployerID { get; set; }
        public System.DateTime TimePosted { get; set; }
        public int CategoryID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Deadline { get; set; }
        public int MinPayment { get; set; }
        public Nullable<int> JobType { get; set; }
        public Nullable<int> MaxPayment { get; set; }
    
        public virtual Attachment Attachment { get; set; }
        public virtual Category Category { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Lookup Lookup { get; set; }

        public HttpPostedFileBase AttachmentFile { get; set; }
    }
}
