using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitiesManager.Business;
using ActivitiesManager.Business.Common;
using ActivitiesManager.Data.Connections;
using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ActivitiesManager.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(Configuration);

            services.AddTransient<IBaseDeDatos, BasesDeDatos>();

            services.AddTransient<IActivitiesManagerProvider, ActivitiesManagerProvider>();

            services.AddTransient<IServiceComponent, ProyectoBusiness>();

            services.AddTransient<IServiceBusiness, ServiceBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
