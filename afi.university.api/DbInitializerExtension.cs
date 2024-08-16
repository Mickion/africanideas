﻿using afi.university.domain.Entities;
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
                    Id = new Guid("admin@gmail.com"),
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Admin
                },
                new User
                {
                    Id = new Guid("mickion@gmail.com"),
                    FirstName = "Mthokozisi",
                    LastName = "Mazibuko",
                    Email = "mickion@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Student
                },
                new User
                {
                    Id = new Guid("petunia@gmail.com"),
                    FirstName = "Petunia",
                    LastName = "Mazibuko",
                    Email = "petunia@gmail.com",
                    Password = "pass",
                    Role = domain.Common.Enums.UserRole.Lecture
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
                    Id=new Guid("BSC Bachelor Of Computer Science"),
                    Name= "BSC Bachelor Of Computer Science",
                    NQFLevel = 7,
                    Duration = 4
                },
                new Course
                {
                    Id = new Guid("National Diploma Information Technology"),
                    Name = "National Diploma Information Technology",
                    NQFLevel = 6,
                    Duration = 3
                },
                new Course
                {
                    Id = new Guid("BSC Chemical engineering"),
                    Name = "BSC Chemical engineering",
                    NQFLevel = 7,
                    Duration = 5
                },
                new Course
                {
                    Id = new Guid("National Diploma Civil engineering"),
                    Name = "National Diploma Civil engineering",
                    NQFLevel = 6,
                    Duration = 5
                }
            };

            foreach (var course in courses)
                dbContext.Courses!.Add(course);

            dbContext.SaveChanges();
        }

    }
}
