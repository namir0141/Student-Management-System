using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : Controller
    {
        
        private readonly string _connectionString;

        // Constructor to initialize the connection string using IConfiguration

        public StudentController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all students from the database
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllstudents()
        {
            // Create a list to hold student data
            List<Students> student = new List<Students>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllStudents", connection))
                {
                    // Specify that the command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Read data from the database and populate the student list
                    while (reader.Read())
                    {
                        // Create a new student object and populate it from the database
                        Students stu = new Students();
                        stu.Student_ID = (int)reader["Student_ID"];
                        stu.FirstName = reader["FirstName"].ToString();
                        stu.LastName = reader["LastName"].ToString();
                        stu.Age = (int)reader["Age"];
                        stu.CourseID = (int)reader["CourseID"];

                        // Add the student to the list
                        student.Add(stu);
                    }
                }
            }
            // Return the list of courses as JSON
            return Ok(student);
        }

        // Get students from the database by Student ID
        [HttpGet("GetStudent/{id}")]
        public IActionResult GetStudentById(int id)
        {
            Students student = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        student = new Students();
                        student.Student_ID = (int)reader["Student_ID"];
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Age = (int)reader["Age"];
                        student.CourseID = (int)reader["CourseID"];
                    }
                }
            }

            if (student == null)
            {
                return NotFound(); // Return a 404 Not Found if the student is not found
            }

            return Ok(student);
        }


        // Add a new student to the database
        [HttpPost("AddStudent")]
        public IActionResult AddStudents(Students stu)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("AddStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", stu.FirstName);
                command.Parameters.AddWithValue("@LastName", stu.LastName);
                command.Parameters.AddWithValue("@Age", stu.Age);
                command.Parameters.AddWithValue("@CourseID", stu.CourseID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        return Ok();
    }

        // Update a student Information to the database
        [HttpPut("UpdateStudent/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Students updatedStudent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", id);
                    command.Parameters.AddWithValue("@FirstName", updatedStudent.FirstName);
                    command.Parameters.AddWithValue("@LastName", updatedStudent.LastName);
                    command.Parameters.AddWithValue("@Age", updatedStudent.Age);
                    command.Parameters.AddWithValue("@CourseID", updatedStudent.CourseID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Ok();
        }

        // Delete a student to the database
        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult DeleteStudents(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        // Get all the information of students olden than 20
        [HttpGet("older-than-20")]
        public IActionResult GetStudentsOlderThan20()
        {
            List<Students> students = new List<Students>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Olderthen20", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Students student = new Students();
                        student.Student_ID = (int)reader["Student_ID"];
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Age = (int)reader["Age"];
                        student.CourseID = (int)reader["CourseID"];
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }

        // Get all the Students Enrolled in a specific Course
        [HttpGet("enrolled-in-course/{courseId}")]
        public IActionResult GetStudentsEnrolledInCourse(int courseId)
        {
            List<Students> students = new List<Students>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetStudentsEnrolledInCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Students student = new Students();
                        student.Student_ID = (int)reader["Student_ID"];
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Age = (int)reader["Age"];
                        student.CourseID = (int)reader["CourseID"];
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }







    }
}
