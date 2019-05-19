using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repositories;
using MoviesAPI.Services;
using MoviesAPI.Validation;
using MoviesAPI.Validation.Validators;

namespace MoviesAPI
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
            services.AddMvc().AddControllersAsServices()
                .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("MoviesList"));

            //Register Validators
            services.AddSingleton(typeof(IValidator<AddMovieRating>), typeof(AddMovieRatingValidator));
            services.AddSingleton(typeof(IValidator<MovieRequest>), typeof(MovieRequestValidator));

            //Register Repositories and Services
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieService, MovieService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
