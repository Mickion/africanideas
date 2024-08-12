using afi.university.domain.Common.Enums;
using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.infrastructure.Persistence;

namespace afi.university.api
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                Initialize(context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }

        internal static void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Users!.Any()) return;

            var users = new Student[]
            {
               new Student{ Id = 1, FirstName = "Mickion", LastName="Mazibuko", Email="mickion.mtshali@gmail.com", Password="test", Role=UserRole.Admin, StudentNumber="" }            
            };

            foreach (var user in users)
                dbContext.Students!.Add(user);

            dbContext.SaveChanges();

            var courses = new Course[]
            {
                new Course{ Id = 1, Name="ND: Information Technology", Duration=3},
                new Course{ Id = 2, Name="BSC Human Resources", Duration=4},
                new Course{ Id = 3, Name="MBA Business Management", Duration=6}
            };

            foreach (var course in courses)
                dbContext.Courses!.Add(course);

            dbContext.SaveChanges();
        }
    }
}
