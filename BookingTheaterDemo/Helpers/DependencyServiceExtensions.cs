using BookingTheaterDemo.Interfaces;
using BookingTheaterDemo.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Helpers
{
    public static class DependencyServiceExtensions
    {
        public static IServiceCollection AddDependencyService(this IServiceCollection services)
        {
            //services.AddSingleton(provide =>)
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ITheaterRepository, TheaterRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            return services;

        }
    }
}
