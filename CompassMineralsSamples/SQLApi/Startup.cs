using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Added Code 01
/// Importing Entity Framework Package to the class (Like Classpath) 
/// </summary>

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Added Code 02
/// Importing DataContext class (Like Classpath) 
/// </summary>
/// 
using SQLApi.Data;

namespace SQLApi
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
            /// <summary>
            /// Added Code 03
            /// Adding the DataContext class as a service to the netCore initialization proccess 
            /// Creating the injection to the DataContext class constructor (options parameter). 
            /// </summary>
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /// <summary>
            /// Added Code 03
            /// Manage the connection scope 
            /// </summary>
            services.AddScoped<DataContext, DataContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
