----------Store Procedure For Students
-- GET A List of All Students
CREATE PROCEDURE GetAllStudents
AS
BEGIN
    SELECT * FROM Students;
END;
GO

-- GET A List of Student by ID
CREATE PROCEDURE GetStudentById
@Student_ID INT
AS
BEGIN
    SELECT * FROM Students
    WHERE Student_ID = @Student_ID;
END;
GO

-- ADD Sutdents
CREATE PROCEDURE AddStudents
    @FirstName VARCHAR(50),
	@LastName VARCHAR(50),
    @Age INT,
    @CourseID INT
AS
BEGIN
    INSERT INTO Students(FirstName, LastName, Age, CourseID)
    VALUES (@FirstName,@LastName,@Age,@CourseID );
END;
GO

-- Update Students
CREATE PROCEDURE UpdateStudent
@Student_ID INT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Age INT,
@CourseID INT
AS
BEGIN
    UPDATE Students
    SET FirstName = @FirstName, LastName = @LastName, Age = @Age, CourseID = @CourseID
    WHERE Student_ID = @Student_ID;
END;
GO

-- Delete Students
CREATE PROCEDURE DeleteStudents
    @ID INT
AS
BEGIN
    DELETE FROM Students
    WHERE Student_ID = @ID;
END;
GO

-- Create A list of Students Older then 20
CREATE PROCEDURE Olderthen20
AS
BEGIN
    select * from Students st where st.Age > 20;
END;
GO

-- Create A list of Students Enrolled in a Specific Course
CREATE PROCEDURE GetStudentsEnrolledInCourse
@CourseID INT
AS
BEGIN
    SELECT * FROM Students WHERE CourseID = @CourseID;
END;
GO