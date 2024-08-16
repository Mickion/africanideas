using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace afi.university.infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Lecture>? Lectures { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<StudentCourse>? StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(studentCourse => new { studentCourse.StudentId, studentCourse.CourseId });

            // seed test data
            // no database migration, we are using in-memory db.
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }
    }
}
