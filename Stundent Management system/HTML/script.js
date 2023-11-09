// Student array
var students = [];

// Course array
var courses = [];

// Add Student
function addStudent() {
    var firstName = $('#FirstName').val();
    var lastName = $('#LastName').val();
    var age = $('#age').val();
    var courseId = $('#course').val();

    var student = {
        FirstName: firstName,
        LastName: lastName,
        Age: age,
        CourseID: courseId
    };

     $.ajax({
        url: 'https://localhost:7096/api/Student/AddStudent',
        type: 'POST',
        data: JSON.stringify(student),
        contentType: 'application/json',
        success: function() {
            // Clear form
            $('#studentForm')[0].reset();
            // Update Student List
            fetchStudent();
        }
    });
}

// Update Student
function updateStudent() {
    var student_ID = $('#updateStudentId').val();
    var updatedFirstName = $('#updatedFirstName').val();
    var updatedLastName = $('#updatedLastName').val();
    var updatedAge = $('#updatedAge').val();
    var updatedCourse = $('#updatedCourse').val();

    var student = {
        FirstName: updatedFirstName,
        LastName: updatedLastName,
        Age: updatedAge,
        CourseID: updatedCourse
    };

    $.ajax({
        url: 'https://localhost:7096/api/Student/UpdateStudent/' + student_ID, // Replace with the actual endpoint
        type: 'PUT', // Use the HTTP method for updating data
        data: JSON.stringify(student),
        contentType: 'application/json',
        success: function() {
            // Clear the form
            $('#updateStudentForm')[0].reset();
            // Update Student List
            fetchStudent();
        }
    });
}



// Delete Student
function deleteStudent() {
    var student_ID = $('#deleteStudentId').val();

    $.ajax({
        url: 'https://localhost:7096/api/Student/DeleteStudent/' + student_ID, // Replace with the actual endpoint
        type: 'DELETE',
        success: function() {
            // Handle success (e.g., remove the student from the list or reload the list)
            fetchStudent();
        },
        error: function() {
            // Handle errors, e.g., show an error message
            console.log('Error: Unable to delete the student.');
        }
    });
}



// Add Course
function addCourse() {
    var courseName = $('#CourseName').val();

    var course = {
        CourseName: courseName
    };

    $.ajax({
        url: 'https://localhost:7096/api/Courses/AddCourses', // Replace with the actual endpoint
        type: 'POST', // Use the HTTP method for creating data
        data: JSON.stringify(course),
        contentType: 'application/json',
        success: function() {
            // Clear the form
            $('#CourseForm')[0].reset();
            // Update Course List
            fetchCourses();
        },
        error: function() {
            // Handle errors, e.g., show an error message
            console.log('Error: Unable to add the course.');
        }
    });
}



function updateCourse() {
    var courseId = $('#updateCourseId').val();
    var updatedCourseName = $('#updatedCourseName').val();

    var course = {
        CourseName: updatedCourseName
    };

    $.ajax({
        url: 'https://localhost:7096/api/Courses/UpdateCourses/' + courseId, // Replace with the actual endpoint
        type: 'PUT', // Use the HTTP method for updating data
        data: JSON.stringify(course),
        contentType: 'application/json',
        success: function() {
            // Clear the form
            $('#updateCourseForm')[0].reset();
            // Update Course List
            fetchCourses();
        },
        error: function() {
            // Handle errors, e.g., show an error message
            console.log('Error: Unable to update the course.');
        }
    });
}






// Delete Course
function deleteCourse() {
    var courseId = $('#deleteCourseId').val();

    $.ajax({
        url: 'https://localhost:7096/api/Courses/DeleteCourses/' + courseId, // Replace with the actual endpoint
        type: 'DELETE',
        success: function() {
            // Clear the form
            $('#deleteCourseForm')[0].reset();
            // Update Course List
            fetchCourses();
        },
        error: function() {
            // Handle errors, e.g., show an error message
            console.log('Error: Unable to delete the course.');
        }
    });
}





function fetchStudent() {
    $.ajax({
        url: 'https://localhost:7096/api/student/GetAllStudents',
        type: 'GET',
        success: function(students) { // Rename the parameter to 'students'
            $('#StudentList').empty();
            students.forEach(function(student, index) {
                $('#StudentList').append('<li>' + 'ID:' + student.student_ID  +', '+ student.firstName + ' ' + student.lastName + ', Age: ' + student.age + ', Course ID: ' + student.courseID + '</li>');
            });
        } // Add a closing brace here
    }); // Add a closing parenthesis here
}




function fetchCourses() {
    $.ajax({
        url: 'https://localhost:7096/api/courses/GetAllCourses',
        type: 'GET',
        success: function(data) {
            $('#CourseList').empty();
            data.forEach(function(course, index) { // Use 'data' instead of 'courses'
                $('#CourseList').append('<li>' + 'ID:' + course.courseID + ',CourseName: ' + course.courseName + '</li>');
            });
        }
    });
}




// Initial fetch
$(document).ready(function() {
    fetchStudent();
    fetchCourses();
});

