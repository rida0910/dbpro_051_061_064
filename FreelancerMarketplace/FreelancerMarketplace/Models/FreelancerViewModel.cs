using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerMarketplace.Models
{
    public class JobViewModel
    {
       public Job Job { get; set; }
       public Bid Bid { get; set; }

    }

    public class MyJobsViewModel
    {
        public Job Job { get; set; }
        public Employer Employer { get; set; }

    }

    public class BiddersViewModel
    {
        public IEnumerable<Bid> Bids { get; set; }
        public IEnumerable<Person> People { get; set; }
        public Job Job { get; set; }
        public IEnumerable<AspNetUser> AspNetUsers { get; set; }
        

    }

    public class FreelancerViewModel
    {
        public Person Person { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public Freelancer Freelancer { get; set; }
        public IEnumerable<FreelancerSkill> FreelancerSkill { get; set; }
    }

    public class EmployerViewModel
    {
        public Person Person { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public Employer Employer { get; set; }
        public Company Company { get; set; }
    }

    public class JobSearchViewModel
    {
        public IEnumerable<Job> JobTitle { get; set; }
        public IEnumerable<Job> JobSkill { get; set; }
        public IEnumerable<Job> JobCategory { get; set; }
    }
}