﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB66Entities : DbContext
    {
        public DB66Entities()
            : base("name=DB66Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Freelancer> Freelancers { get; set; }
        public virtual DbSet<Freelancerlanguage> Freelancerlanguages { get; set; }
        public virtual DbSet<FreelancerService> FreelancerServices { get; set; }
        public virtual DbSet<FreelancerSkill> FreelancerSkills { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobDone> JobDones { get; set; }
        public virtual DbSet<JobSkill> JobSkills { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}