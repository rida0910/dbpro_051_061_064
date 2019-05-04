
CREATE VIEW EmployersList 
AS
SELECT FirstName + ' ' + LastName AS EmployersName, UserName, Email, [Address],  CompanyName FROM Person 
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
JOIN Company ON Employer.CompanyId = Company.CompanyId 

/*CREATE Procedure EmployersActiveProjects
AS
SELECT FirstName + ' ' + LastName AS EmployerName, Title AS JobTitle, Deadline FROM Person
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
JOIN Job ON Employer.EmployerId = Job.EmployerID
WHERE JobId NOT IN (SELECT JobId FROM JobDone)*/

CREATE VIEW EmployersActiveProjects
AS
SELECT FirstName + ' ' + LastName AS EmployerName, Title AS JobTitle, Deadline FROM Person
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
WHERE Active = 1


CREATE VIEW FreelancersOfAProject 
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName, UserName FROM Job
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
SELECT UserName AS Freelancer, FirstName + ' ' + LastName AS EmployerName FROM AspNetUsers
JOIN Person ON AspNetUsers.Id = Person.User_AccountID
JOIN Bid ON Person.PersonId = Bid.FreelancerID
JOIN Job ON Bid.JobID = Job.JobId
JOIN Employer ON Job.EmployerID = Employer.EmployerId




CREATE Procedure FreelancersOfAProject @JobTitle varchar(100)
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName, UserName FROM Job
JOIN 


WHERE Title = @JobTitle












COUNT(Email) AS TotalFreelancers ,