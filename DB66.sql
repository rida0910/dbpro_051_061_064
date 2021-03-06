USE [DB66]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[AttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bid]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bid](
	[BidId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [int] NOT NULL,
	[FreelancerID] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[BidTime] [datetime] NOT NULL,
	[PaymentAmount] [int] NULL,
	[DeliveryTime] [datetime] NULL,
	[Accepted] [bit] NULL,
 CONSTRAINT [PK_Bid] PRIMARY KEY CLUSTERED 
(
	[BidId] ASC,
	[FreelancerID] ASC,
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employer]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employer](
	[EmployerId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED 
(
	[EmployerId] ASC,
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Freelancer]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Freelancer](
	[FreelancerId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ExperienceInCategory] [int] NOT NULL,
	[ProfessionalTitle] [nvarchar](100) NOT NULL,
	[ProfessionalOverview] [nvarchar](max) NOT NULL,
	[JobType] [int] NOT NULL,
 CONSTRAINT [PK_Freelancer] PRIMARY KEY CLUSTERED 
(
	[FreelancerId] ASC,
	[CategoryId] ASC,
	[ExperienceInCategory] ASC,
	[JobType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Freelancerlanguage]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Freelancerlanguage](
	[FreelancerId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_Freelancerlanguage] PRIMARY KEY CLUSTERED 
(
	[FreelancerId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FreelancerService]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreelancerService](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Fee] [int] NOT NULL,
	[PicturePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC,
	[CategoryId] ASC,
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FreelancerSkill]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreelancerSkill](
	[SkillId] [int] NOT NULL,
	[FreelancerId] [int] NOT NULL,
 CONSTRAINT [PK_FreelancerSkill] PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC,
	[FreelancerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Job]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[MinPayment] [int] NOT NULL,
	[AttachmentID] [int] NULL,
	[SkillID] [int] NOT NULL,
	[EmployerID] [int] NOT NULL,
	[TimePosted] [datetime] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Deadline] [datetime] NULL,
	[JobType] [int] NULL,
	[MaxPayment] [int] NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[SkillID] ASC,
	[EmployerID] ASC,
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobDone]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobDone](
	[JobId] [int] NOT NULL,
	[FilesReceived] [bit] NOT NULL,
	[PaymentReceived] [bit] NOT NULL,
 CONSTRAINT [PK_JobDone] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobSkill]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobSkill](
	[JobId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
 CONSTRAINT [PK_JobSkill] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[value] [varchar](50) NOT NULL,
	[category] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Message]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AttachmentId] [int] NULL,
	[EmployerId] [int] NOT NULL,
	[FreelancerId] [int] NOT NULL,
	[Time] [datetime] NULL,
	[SentBy] [int] NULL,
 CONSTRAINT [PK_Message_1] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[User_AccountID] [nvarchar](128) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NULL,
	[Gender] [int] NULL,
	[Nationality] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Approved] [bit] NOT NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[AccountType] [varchar](50) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[User_AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillId] [int] IDENTITY(1,1) NOT NULL,
	[SkillName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[EmployersActiveProjects]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EmployersActiveProjects]
AS
SELECT E.FirstName + ' ' + E.LastName AS EmployerName, Title AS JobTitle, F.FirstName + ' ' + F.LastName AS FreelancerName , convert(varchar, Deadline, 106) AS DeadLine FROM Person AS E
JOIN Employer ON E.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person AS F ON F.PersonId = Freelancer.FreelancerId
WHERE Active = 1
GO
/****** Object:  View [dbo].[EmployersList]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EmployersList] 
AS
SELECT FirstName + ' ' + LastName AS EmployerName, Email, Value AS Gender, Nationality,  CompanyName AS Company FROM Person 
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
JOIN Company ON Employer.CompanyId = Company.CompanyId
JOIN [LookUp] ON Person.Gender = [LookUp].Id
GO
/****** Object:  View [dbo].[FreelancersOfAProject]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FreelancersOfAProject] 
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName, Email FROM Job
JOIN Bid ON Job.JobId = Bid.JobID 
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person ON Freelancer.FreelancerId = Person.PersonId
JOIN AspNetUsers ON Person.User_AccountID = AspNetUsers.Id
WHERE Accepted = 'True' AND Active = 1
GO
/****** Object:  View [dbo].[FreelancersUnaaceptedProjects]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FreelancersUnaaceptedProjects] 
AS
SELECT Title AS ProjectTitle, FirstName + ' ' + LastName AS FreelancerName FROM Job 
JOIN Bid ON Job.JobId = Bid.JobID
JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
JOIN Person ON Freelancer.FreelancerId = Person.PersonId
WHERE Accepted = 'False' AND Active = 0
GO
/****** Object:  View [dbo].[FreelancerWorkedWithEmp]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FreelancerWorkedWithEmp]
AS
SELECT F.FirstName + ' ' + F.LastName AS Freelancer, E.FirstName + ' ' + E.LastName AS EmployerName, Title AS JobTitle
FROM Person F
JOIN Freelancer ON F.PersonId = Freelancer.FreelancerId
JOIN Bid ON Freelancer.FreelancerId = Bid.FreelancerID
JOIN Job ON Bid.JobID = Job.JobId
JOIN Employer ON Job.EmployerID = Employer.EmployerId
JOIN Person E ON Employer.EmployerId = E.PersonId

GO
/****** Object:  View [dbo].[InactiveProjectsOfEmployers]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InactiveProjectsOfEmployers] AS
SELECT FirstName + ' ' + LastName AS EmployerName, Title , [Description] FROM Person
JOIN Employer ON Person.PersonId = Employer.EmployerId
JOIN Job ON Employer.EmployerId = Job.EmployerID
JOIN Bid ON Job.JobId = Bid.JobID 
WHERE Active = 0 and Accepted = 1
GO
/****** Object:  View [dbo].[JobsDoneByFreelancers]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[JobsDoneByFreelancers] AS
 SELECT FirstName + ' ' + LastName AS [Freelancer Name], title AS [Job Title], [Description] FROM Job
 JOIN Bid ON Job.JobId = Bid.JobID
 JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
 JOIN Person ON Person.PersonId = Freelancer.FreelancerId
 where active = 0 and Accepted = 1
GO
/****** Object:  View [dbo].[JobsPostedByEmployer]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[JobsPostedByEmployer] AS
 SELECT FirstName + ' ' + LastName AS EmployerName, Title, MinPayment AS [Minimum Payment], MaxPayment AS [Maximum Payment], CategoryName AS [Job Category] FROM Job 
 JOIN Employer ON Job.EmployerID = Employer.EmployerId
 JOIN Person ON Job.EmployerID = Person.PersonId
 JOIN Category ON Job.CategoryID = Category.CategoryId
GO
/****** Object:  View [dbo].[ListOfFreelancers]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ListOfFreelancers] AS
 SELECT FirstName + ' ' + LastName AS Name, ProfessionalTitle, ProfessionalOverview, Value AS Gender, Nationality, [Address] FROM Person
 JOIN Freelancer ON Person.PersonId = Freelancer.FreelancerId
 JOIN [Lookup] ON Person.Gender = Lookup.Id
GO
/****** Object:  View [dbo].[NoOfProjectOfFreelancerAndEmployer]    Script Date: 5/6/2019 4:35:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NoOfProjectOfFreelancerAndEmployer] AS
 SELECT E.FirstName + ' '+ E.LastName AS [Employer Name], F.FirstName + ' ' + F.LastName AS [Freelancer Name],
 Count(BidId) AS [Number of accepted projects] FROM Person E JOIN Job ON E.PersonId = Job.EmployerID
 JOIN Bid ON Bid.JobID = Job.JobId
 JOIN Freelancer ON Bid.FreelancerID = Freelancer.FreelancerId
 JOIN Person F ON F.PersonId = Freelancer.FreelancerId
 Where Accepted = 1
 group by E.FirstName + ' '+ E.LastName, F.FirstName + ' ' + F.LastName
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EMPLOYERID]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EMPLOYERID] ON [dbo].[Employer]
(
	[EmployerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FreelancerId]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_FreelancerId] ON [dbo].[Freelancer]
(
	[FreelancerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Job]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Job] ON [dbo].[Job]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonID]    Script Date: 5/6/2019 4:35:32 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PersonID] ON [dbo].[Person]
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_Freelancer] FOREIGN KEY([FreelancerID])
REFERENCES [dbo].[Freelancer] ([FreelancerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_Freelancer]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_Job]
GO
ALTER TABLE [dbo].[Employer]  WITH CHECK ADD  CONSTRAINT [FK_Employer_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employer] CHECK CONSTRAINT [FK_Employer_Company]
GO
ALTER TABLE [dbo].[Freelancer]  WITH CHECK ADD  CONSTRAINT [FK_Freelancer_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Freelancer] CHECK CONSTRAINT [FK_Freelancer_Category]
GO
ALTER TABLE [dbo].[Freelancer]  WITH CHECK ADD  CONSTRAINT [FK_Freelancer_ExperienceLookup] FOREIGN KEY([ExperienceInCategory])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Freelancer] CHECK CONSTRAINT [FK_Freelancer_ExperienceLookup]
GO
ALTER TABLE [dbo].[Freelancer]  WITH CHECK ADD  CONSTRAINT [FK_Freelancer_JobTypeLookup] FOREIGN KEY([JobType])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Freelancer] CHECK CONSTRAINT [FK_Freelancer_JobTypeLookup]
GO
ALTER TABLE [dbo].[Freelancer]  WITH CHECK ADD  CONSTRAINT [FK_Freelancer_Person] FOREIGN KEY([FreelancerId])
REFERENCES [dbo].[Person] ([PersonId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Freelancer] CHECK CONSTRAINT [FK_Freelancer_Person]
GO
ALTER TABLE [dbo].[Freelancerlanguage]  WITH CHECK ADD  CONSTRAINT [FK_Freelancerlanguage_Freelancr] FOREIGN KEY([FreelancerId])
REFERENCES [dbo].[Freelancer] ([FreelancerId])
GO
ALTER TABLE [dbo].[Freelancerlanguage] CHECK CONSTRAINT [FK_Freelancerlanguage_Freelancr]
GO
ALTER TABLE [dbo].[Freelancerlanguage]  WITH CHECK ADD  CONSTRAINT [FK_Freelancerlanguage_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[Freelancerlanguage] CHECK CONSTRAINT [FK_Freelancerlanguage_Language]
GO
ALTER TABLE [dbo].[FreelancerService]  WITH CHECK ADD  CONSTRAINT [FK_FreelancerService_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[FreelancerService] CHECK CONSTRAINT [FK_FreelancerService_Category]
GO
ALTER TABLE [dbo].[FreelancerService]  WITH CHECK ADD  CONSTRAINT [FK_FreelancerService_Skill] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([SkillId])
GO
ALTER TABLE [dbo].[FreelancerService] CHECK CONSTRAINT [FK_FreelancerService_Skill]
GO
ALTER TABLE [dbo].[FreelancerSkill]  WITH CHECK ADD  CONSTRAINT [FK_FreelancerSkill_Freelancer] FOREIGN KEY([FreelancerId])
REFERENCES [dbo].[Freelancer] ([FreelancerId])
GO
ALTER TABLE [dbo].[FreelancerSkill] CHECK CONSTRAINT [FK_FreelancerSkill_Freelancer]
GO
ALTER TABLE [dbo].[FreelancerSkill]  WITH CHECK ADD  CONSTRAINT [FK_FreelancerSkill_Skill] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([SkillId])
GO
ALTER TABLE [dbo].[FreelancerSkill] CHECK CONSTRAINT [FK_FreelancerSkill_Skill]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Attachment] FOREIGN KEY([AttachmentID])
REFERENCES [dbo].[Attachment] ([AttachmentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Attachment]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Category]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Employer] FOREIGN KEY([EmployerID])
REFERENCES [dbo].[Employer] ([EmployerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Employer]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_JobTypeLookUp] FOREIGN KEY([JobType])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_JobTypeLookUp]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([SkillId])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_skill]
GO
ALTER TABLE [dbo].[JobDone]  WITH CHECK ADD  CONSTRAINT [FK_JobDone_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[JobDone] CHECK CONSTRAINT [FK_JobDone_Job]
GO
ALTER TABLE [dbo].[JobSkill]  WITH CHECK ADD  CONSTRAINT [FK_JobSkill_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[JobSkill] CHECK CONSTRAINT [FK_JobSkill_Job]
GO
ALTER TABLE [dbo].[JobSkill]  WITH CHECK ADD  CONSTRAINT [FK_JobSkill_Skill] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([SkillId])
GO
ALTER TABLE [dbo].[JobSkill] CHECK CONSTRAINT [FK_JobSkill_Skill]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Attachment] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachment] ([AttachmentId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Attachment]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Employer] FOREIGN KEY([EmployerId])
REFERENCES [dbo].[Employer] ([EmployerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Employer]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Freelancer] FOREIGN KEY([FreelancerId])
REFERENCES [dbo].[Freelancer] ([FreelancerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Freelancer]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD FOREIGN KEY([Gender])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD FOREIGN KEY([User_AccountID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK__Person__User_Acc__5AEE82B9] FOREIGN KEY([User_AccountID])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK__Person__User_Acc__5AEE82B9]
GO
USE [master]
GO
ALTER DATABASE [DB66] SET  READ_WRITE 
GO
