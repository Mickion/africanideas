using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace afi.university.application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure dependency injections
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //    cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            //    cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            //    cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            //    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            //    cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            //});
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddScoped<IValidatorProvider, ValidatorProvider>();
            //services.AddTransient<ICustomerAccountService, CustomerAccountService>();
            return services;
        }
    }
}
