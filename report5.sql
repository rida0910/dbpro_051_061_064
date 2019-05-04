CREATE VIEW FreelancerWorkedWithEmpl
AS
SELECT F.FirstName + ' ' + F.LastName AS Freelancer, E.FirstName + ' ' + E.LastName AS EmployerName FROM Person F
JOIN Freelancer ON F.PersonId = Freelancer.FreelancerId
JOIN Bid ON Freelancer.FreelancerId = Bid.FreelancerID
JOIN Job ON Bid.JobID = Job.JobId
JOIN Employer ON Job.EmployerID = Employer.EmployerId
JOIN Person E ON Employer.EmployerId = E.PersonId