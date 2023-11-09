--Store Procedures for Courses

-- Get a List of All courses
CREATE PROCEDURE GetAllCourses
AS
BEGIN
    SELECT * FROM Courses;
END;
GO

-- Get a List of courses by ID
CREATE PROCEDURE GetCourseById
@CourseID INT
AS
BEGIN
    SELECT * FROM Courses
    WHERE CourseID = @CourseID;
END;
GO


-- Add Courses
CREATE PROCEDURE AddCourses
	@CourseName VARCHAR(50)
AS
BEGIN
    INSERT INTO Courses( CourseName)
    VALUES (@CourseName );
END;
GO

--Update Course
CREATE PROCEDURE UpdateCourses
    @ID INT,
    @NewName VARCHAR(50)
AS
BEGIN
    UPDATE Courses
    SET CourseName = @NewName
    WHERE CourseID = @ID;
END;
GO

--Delete Courses
CREATE PROCEDURE DeleteCourses
    @ID INT
AS
BEGIN
    DELETE FROM Courses
    WHERE CourseID = @ID;
END;
GO

--  find the most popular course.
CREATE PROCEDURE FindMostPopularCourse
AS
BEGIN
    SELECT TOP 1 c.CourseID, c.CourseName
    FROM Courses c
    LEFT JOIN Students s ON c.CourseID = s.CourseID
    GROUP BY c.CourseID, c.CourseName
    ORDER BY COUNT(s.Student_ID) DESC;

END;
GO
