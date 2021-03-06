using CatecVisitas.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatecVisitas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var appSettings = Configuration.GetSection("Authorized:Users");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires 
            // using RazorPagesMovie.Models;
            // using Microsoft.EntityFrameworkCore;
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<PersonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PersonContext")));
            services.AddMvc().AddRazorPagesOptions(options =>
             {
                 options.Conventions.AuthorizeFolder("/Visitas");
                 options.Conventions.AuthorizeFolder("/Visitantes");
                 options.Conventions.AuthorizeFolder("/EnlaceVisitaPersona");
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
