using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movies.Contract;
using Movies.Models;
using Movies.MoviesRepository;


namespace Movies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IMoviesIMDB, MoviesIMDB>();
            services.AddScoped<IMovie, BL.Movie>();
            services.AddScoped<ICategory, BL.Category>();
            services.AddScoped<Contract.IUser, BL.User>();
            services.AddSingleton<IMovieDatabaseSettings, MovieDatabaseSettings>();
            services.AddSingleton<ICategoryDatabaseSettings, CategoryDatabaseSettings>();
            services.AddScoped<ICategoryIMDB, CategoryIMDB>();
            services.AddScoped<IUserDB, UserDB>();
            services.AddSingleton<IUserDatabaseSettings, UserDatabaseSettings>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
