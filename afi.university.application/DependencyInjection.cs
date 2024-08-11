﻿
using Microsoft.Extensions.DependencyInjection;
using afi.university.application.Services.Interfaces;
using afi.university.application.Services.Implementation;

namespace afi.university.application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds application dependency injections
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentService, StudentService>();
            
            return services;
        }
    }
}
