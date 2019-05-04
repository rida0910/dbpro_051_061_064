CREATE VIEW JobsDoneByFreelancers AS
 SELECT FirstName + ' ' + LastName AS [Freelancer Name], title AS [Job Title], [Description] FROM Job
 JOIN Bid ON Job.JobId = Bid.JobID
 JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
 JOIN Person ON Person.PersonId = Freelancer.FreelancerId
 where active = 0 and Accepted = 1
 
 CREATE VIEW NoOfProjectOfFreelancerAndEmployer AS
 SELECT E.FirstName + ' '+ E.LastName AS [Employer Name], F.FirstName + ' ' + F.LastName AS [Freelancer Name],
 Count(BidId) AS [Number of accepted projects] FROM Person E JOIN Job ON E.PersonId = Job.EmployerID
 JOIN Bid ON Bid.JobID = Job.JobId
 JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
 JOIN Person F ON F.PersonId = Freelancer.FreelancerId
 Where Accepted = 1
 group by E.FirstName + ' '+ E.LastName, F.FirstName + ' ' + F.LastName

 
CREATE VIEW InactiveProjectsOfEmployers AS
SELECT FirstName + ' ' + LastName AS EmployerName, Title , [Description] FROM Person
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
WHERE Active = 0 and Accepted = 1


 CREATE VIEW JobsPostedByEmployer AS
 SELECT FirstName + ' ' + LastName AS EmployerName, Title, MinPayment AS [Minimum Payment], MaxPayment AS [Maximum Payment], CategoryName AS [Job Category] FROM Job 
 JOIN Employer ON Job.EmployerID = Employer.EmployerId
 JOIN Person ON Job.EmployerID = Person.PersonId
 JOIN Category ON Job.CategoryID = Category.CategoryId
 
 
 CREATE VIEW ListOfFreelancers AS
 SELECT FirstName + ' ' + LastName AS Name, ProfessionalTitle, ProfessionalOverview, Value AS Gender, Nationality, [Address] FROM Person
 JOIN Freelancer ON Person.PersonId = Freelancer.FreelancerId
 JOIN [Lookup] ON Person.Gender = Lookup.Id


