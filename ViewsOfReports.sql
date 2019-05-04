
CREATE VIEW EmployersList 
AS
SELECT FirstName + ' ' + LastName AS EmployerName, Email, Value AS Gender, Nationality,  CompanyName AS Company FROM Person 
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
JOIN Company ON Employer.CompanyId = Company.CompanyId
JOIN [LookUp] ON Person.Gender = [LookUp].Id


CREATE VIEW EmployersActiveProjects
AS
SELECT E.FirstName + ' ' + E.LastName AS EmployerName, Title AS JobTitle, F.FirstName + ' ' + F.LastName AS FreelancerName , convert(varchar, Deadline, 106) AS DeadLine FROM Person AS E
JOIN Employer ON E.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person AS F ON F.PersonId = Freelancer.FreelancerId
WHERE Active = 1


CREATE VIEW FreelancersOfAProject 
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName, Email FROM Job
JOIN Bid ON Job.JobId = Bid.JobID 
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person ON Freelancer.FreelancerId = Person.PersonId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
WHERE Accepted = 'True' AND Active = 1

CREATE VIEW FreelancersUnaaceptedProjects 
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName FROM Job 
JOIN Bid ON Job.JobId = Bid.JobID
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person ON Freelancer.FreelancerId = Person.PersonId
WHERE Accepted = 'False' AND Active = 0

CREATE VIEW FreelancerWorkedWithEmp
AS
SELECT F.FirstName + ' ' + F.LastName AS Freelancer, E.FirstName + ' ' + E.LastName AS EmployerName, Title AS JobTitle
FROM Person F
JOIN Freelancer ON F.PersonId = Freelancer.FreelancerId
JOIN Bid ON Freelancer.FreelancerId = Bid.FreelancerID
JOIN Job ON Bid.JobID = Job.JobId
JOIN Employer ON Job.EmployerID = Employer.EmployerId
JOIN Person E ON Employer.EmployerId = E.PersonId














