using afi.university.domain.Common.Enums;
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
            if (dbContext.Users.Any()) return;

            var users = new User[]
            {
                new User{ Id = 1, FirstName = "Mickion", LastName="Mazibuko", Email="test@test.com", Password="test", Role=UserRole.Admin }
            //add other users
            };

            foreach (var user in users)
                dbContext.Users.Add(user);

            dbContext.SaveChanges();
        }
    }
}
