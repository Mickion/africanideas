using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.infrastructure.Persistence;

namespace afi.university.api
{
    internal static class DbInitializerExtension
    {        
        public static IApplicationBuilder UseSeedInMemoryDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                InitializeUsers(context);
                InitializeCourses(context);
                InitializeStudentCourse(context);
            }
            catch (Exception)
            {
                throw;
            }

            return app;
        }

        /// <summary>
        /// Seeds User test data
        /// </summary>
        /// <param name="dbContext"></param>
        private static void InitializeUsers(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Users!.Any()) return;

            var users = new User[]
            {
                new User
                {
                    Id = new Guid(),
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Admin,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new Student
                {
                    Id =new Guid("ec35b9d8-ae83-4c76-9a48-86bb796005bd"),
                    FirstName = "Mthokozisi",
                    LastName = "Mazibuko",
                    Email = "mickion@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Student,
                    StudentNumber = "MthoMaz2024",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now

                },
                new Lecture
                {
                    Id = new Guid(),
                    FirstName = "Petunia",
                    LastName = "Mazibuko",
                    Email = "petunia@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Lecture,
                    YearsOfExperience = 13,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new Student
                {
                    Id = new Guid("2c155c53-1fd3-4a79-9801-aacf52eb3da6"),
                    FirstName = "Cebo",
                    LastName = "Dladla",
                    Email = "cebo@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Student,
                    StudentNumber = "CeboDla2024",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            };

            foreach (var user in users)
                dbContext.Users!.Add(user);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Seeds Course test data
        /// </summary>
        /// <param name="dbContext"></param>
        private static void InitializeCourses(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Courses!.Any()) return;

            var courses = new Course[]
            {
                new Course
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce2"),
                    Name= "BSC Bachelor Of Computer Science",
                    NQFLevel = 7,
                    Duration = 4,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "National Diploma Information Technology",
                    NQFLevel = 6,
                    Duration = 3,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "BSC Chemical engineering",
                    NQFLevel = 7,
                    Duration = 5,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "National Diploma Civil engineering",
                    NQFLevel = 6,
                    Duration = 5,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            };

            foreach (var course in courses)
                dbContext.Courses!.Add(course);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Seeds Student Course test data
        /// </summary>
        /// <param name="dbContext"></param>
        private static void InitializeStudentCourse(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.StudentCourses!.Any()) return;

            var studcourses = new StudentCourse[]
            {
                new StudentCourse
                {
                    StudentId = new Guid("ec35b9d8-ae83-4c76-9a48-86bb796005bd"),
                    CourseId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce2"),
                    DateCreated = DateTime.Now,
                    DateModified= DateTime.Now
                }               
            };

            foreach (var studco in studcourses)
                dbContext.StudentCourses!.Add(studco);

            dbContext.SaveChanges();
        }

    }
}
