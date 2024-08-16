using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using afi.university.domain.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;


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
                options.UseInMemoryDatabase(databaseName: configuration["afi.university.persistence:databaseName"]);
            });

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
