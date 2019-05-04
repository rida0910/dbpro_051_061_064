CREATE VIEW JobsDoneByFreelancers AS
 SELECT title, [Description] FROM Job
 Join Freelancer ON Job.CategoryID = Freelancer.CategoryId
 Join JobDone ON JobDone.JobId = Job.JobId
 
 CREATE VIEW SameEmployersDifferentProjects AS
 SELECT title, [Description] FROM Job 
  Join Freelancer ON Job.CategoryID = Freelancer.CategoryId
 JOIN Employer ON Job.EmployerID = Employer.EmployerId

CREATE VIEW InactiveProjectsOfEmployers AS
SELECT FirstName + ' ' + LastName AS EmployerName, Title , [Description] FROM Person
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
WHERE Active = 0


 CREATE VIEW JobsPostedByEmployer AS
 SELECT Title, [Description] FROM Job 
 JOIN Employer ON Job.EmployerID = Employer.EmployerId

 
 
 CREATE VIEW ListOfFreelancers AS
 SELECT FirstName + ' ' + LastName AS Name, ProfessionalTitle, ProfessionalOverview FROM Person
 JOIN Freelancer ON Person.PersonId = Freelancer.FreelancerId
