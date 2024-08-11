using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace afi.university.infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure dependency injections
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase(databaseName: "AfricanIdeasUniversity");
            });

            services.AddScoped<ApplicationDbContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }
    }
}
