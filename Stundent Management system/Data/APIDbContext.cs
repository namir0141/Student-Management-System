using Microsoft.EntityFrameworkCore;
using System;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        // DbSet properties for your database tables
        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Students>().HasKey(s => s.Student_ID);
            // Other configurations for your entities
        }
    }
}


