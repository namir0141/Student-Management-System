CREATE TABLE Courses(
    CourseID int IDENTITY(1,1) PRIMARY KEY,
    CourseName varchar(50)
    );
	Go

CREATE TABLE Students
  (
  Student_ID int IDENTITY(1,1) primary key,
  FirstName varchar(50),
  LastName varchar(50),
  Age int,
  CourseID int ,
  FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
  );
  Go
  

  
