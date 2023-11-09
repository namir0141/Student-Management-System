using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CoursesController : Controller
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string using IConfiguration
        public CoursesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all Courses from the database
        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            // Create a list to hold Course data
            List<Courses> course = new List<Courses>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllCourses", connection))
                {
                    // Specify that the command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    // Read data from the database and populate the Course list
                    while (reader.Read())
                    {
                        // Create a new Course object and populate it from the database
                        Courses cr = new Courses();
                        cr.CourseID = (int)reader["CourseID"];
                        cr.CourseName = reader["CourseName"].ToString();

                        // Add the Course to the list
                        course.Add(cr);
                    }
                }
            }
            // Return the list of courses as JSON
            return Ok(course);
        }


        // Get Courses from the database by Course ID
        [HttpGet("GetCourses/{id}")]
        public IActionResult GetCourseById(int id)
        {
            Courses course = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetCourseById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Create a new student object and populate it from the database
                        course = new Courses();
                        course.CourseID = (int)reader["CourseID"];
                        course.CourseName = reader["CourseName"].ToString();
                    }
                }
            }

            if (course == null)
            {
                return NotFound(); // Return a 404 Not Found if the course is not found
            }

            return Ok(course);
        }

        // Add a new Course to the database
        [HttpPost("AddCourses")]
        public IActionResult AddStudents(Courses cr)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseName", cr.CourseName);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            // Return a success response
            return Ok();
        }

        // Update a Course Information to the database
        [HttpPut("UpdateCourses/{id}")]
        public IActionResult UpdateCourses(int id, [FromBody] Courses cr)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id); // Provide the 'ID' parameter
                    command.Parameters.AddWithValue("@NewName", cr.CourseName); // Provide the 'NewName' parameter
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            // Return a success response
            return Ok();
        }

        // Delete a Course to the database
        [HttpDelete("DeleteCourses/{id}")]
        public IActionResult DeleteCourses(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            // Return a success response
            return Ok();
        }

        // Find a Most Popular Course
        [HttpGet("most-popular-course")]
        public IActionResult FindMostPopularCourse()
        {
            Courses mostPopularCourse = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("FindMostPopularCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mostPopularCourse = new Courses();
                        mostPopularCourse.CourseID = (int)reader["CourseID"];
                        mostPopularCourse.CourseName = reader["CourseName"].ToString();
                    }
                }
            }
            // Return a success response
            return Ok(mostPopularCourse);
        }







    }
}
