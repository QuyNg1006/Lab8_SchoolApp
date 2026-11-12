-- Lab 8 - School App (Code First schema, tuong duong Add-Migration InitialCreate)
-- Chay trong SSMS. Neu da chay Update-Database thi KHONG can chay file nay (hoac xoa DB truoc).
IF DB_ID(N'SchoolDBMVC') IS NULL CREATE DATABASE [SchoolDBMVC];
GO
USE [SchoolDBMVC];
GO

CREATE TABLE [Departments] (
    [DepartmentID]   INT IDENTITY(1,1) NOT NULL,
    [DepartmentName] NVARCHAR(MAX) NOT NULL,
    [Budget]         DECIMAL(18,2) NOT NULL,
    [StartDate]      DATETIME2 NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentID])
);
CREATE TABLE [Instructors] (
    [InstructorID] INT IDENTITY(1,1) NOT NULL,
    [LastName]     NVARCHAR(MAX) NOT NULL,
    [FirstName]    NVARCHAR(MAX) NOT NULL,
    [HireDate]     DATETIME2 NOT NULL,
    CONSTRAINT [PK_Instructors] PRIMARY KEY ([InstructorID])
);
CREATE TABLE [Students] (
    [StudentID]      INT IDENTITY(1,1) NOT NULL,
    [LastName]       NVARCHAR(MAX) NOT NULL,
    [FirstName]      NVARCHAR(MAX) NOT NULL,
    [EnrollmentDate] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([StudentID])
);
CREATE TABLE [Courses] (
    [CourseID]     INT IDENTITY(1,1) NOT NULL,
    [Title]        NVARCHAR(MAX) NOT NULL,
    [Credits]      INT NOT NULL,
    [DepartmentID] INT NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([CourseID]),
    CONSTRAINT [FK_Courses_Departments_DepartmentID] FOREIGN KEY ([DepartmentID])
        REFERENCES [Departments]([DepartmentID]) ON DELETE CASCADE
);
CREATE TABLE [Enrollments] (
    [EnrollmentID] INT IDENTITY(1,1) NOT NULL,
    [CourseID]     INT NOT NULL,
    [StudentID]    INT NOT NULL,
    [Grade]        DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY ([EnrollmentID]),
    CONSTRAINT [FK_Enrollments_Courses_CourseID] FOREIGN KEY ([CourseID])
        REFERENCES [Courses]([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_Students_StudentID] FOREIGN KEY ([StudentID])
        REFERENCES [Students]([StudentID]) ON DELETE CASCADE
);
CREATE INDEX [IX_Courses_DepartmentID] ON [Courses]([DepartmentID]);
CREATE INDEX [IX_Enrollments_CourseID] ON [Enrollments]([CourseID]);
CREATE INDEX [IX_Enrollments_StudentID] ON [Enrollments]([StudentID]);
GO

-- Du lieu mau
INSERT INTO [Departments] ([DepartmentName],[Budget],[StartDate]) VALUES
 (N'Computer Science', 100000, '2025-04-01'),
 (N'Information Systems', 200000, '2025-04-01');
INSERT INTO [Instructors] ([LastName],[FirstName],[HireDate]) VALUES
 (N'Pham', N'Trung', '2015-09-01'),
 (N'Nguyen', N'An', '2018-02-15');
INSERT INTO [Students] ([LastName],[FirstName],[EnrollmentDate]) VALUES
 (N'Tran', N'Binh', '2024-09-05'),
 (N'Le', N'Chi', '2024-09-05');
INSERT INTO [Courses] ([Title],[Credits],[DepartmentID]) VALUES
 (N'Software Engineering', 3, 1),
 (N'Database Systems', 3, 2);
INSERT INTO [Enrollments] ([CourseID],[StudentID],[Grade]) VALUES
 (1, 1, 8.5),
 (2, 2, 9.0);
GO
