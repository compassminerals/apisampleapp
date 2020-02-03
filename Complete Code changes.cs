// Start Configuring Entity Framework step 1
// "Slides 29 - 32"
// File: DataContext.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Added Code 01
/// Importing Entity Framework Package to the class (Like Classpath) 
/// </summary>
using Microsoft.EntityFrameworkCore;

namespace SQLApi.Data
{
    /// <summary>
    /// Added Code 02
    /// Extend class from entity framework (Use ":" after the class declaration to extend ) 
    /// </summary>

    public class DataContext : DbContext
    {

        /// <summary>
        /// Added Code 03
        /// Creating the class constructor
        /// Pass the main class DataContext options to the base class with dependece injection
        /// Read more about dependence injection
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

    }
}
// End Configuring Entity Framework step 1
////////////////////////////////////////////////////////////////////////////////////////////////////////

// Start Configuring Entity Framework step 2
// "Slides 33 - 35"
// File: Startup.cs

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
// End Configuring Entity Framework step 2
////////////////////////////////////////////////////////////////////////////////////////////////////////




// Start Configuring Entity Framework step 3
// "Slides 36 - 37"
// File: appsetting.json

{
  "ConnectionStrings": {
    "DefaultConnection" : "Server=(localdb)\\mssqllocaldb;database=SQLApiDatabase;Trusted_Connection=True;MultipleActiveResultSets=true" 
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

// End Configuring Entity Framework step 3
////////////////////////////////////////////////////////////////////////////////////////////////////////

// Start Creating the Model Class
// "Slides 42 - 43"
// File: People.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Added Code 01
/// Imports to DataNotations to describe fields properties, according to the database specifications.
/// Read more about DataAnnotations
/// </summary>
/// 

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLApi.Models
{
    /// <summary>
    /// Class that represents the table People on the database 
    /// </summary>
    /// 


    /// <summary>
    /// Added Code 02
    /// 01 Our First DataAnnotation (In Brackets above the class declaration)
    /// This annotation describes this class as a Table class to Entity Framework
    /// </summary>
    /// 
    [Table("People")]
    public class People
    {

        /// <summary>
        /// Added Code 03
        /// 01 Create a property to each collumn of the table 
        /// 02 Annotations for each field propertie - In this sample we're referencing the Id field
        /// as the table primary key
        /// </summary>
        /// 
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
    }
}
// End Creating the Model Class
////////////////////////////////////////////////////////////////////////////////////////////////////////


// Start Linking the model to the table
// "Slides 44 - 44"
// File: DataContext.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Added Code 01
/// Importing Entity Framework Package to the class (Like Classpath) 
/// </summary>
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Added Code 04
/// Importing the model class
/// </summary>
using SQLApi.Models;

namespace SQLApi.Data
{
    /// <summary>
    /// Added Code 02
    /// Extend class from entity framework (Use ":" after the class declaration to extend ) 
    /// </summary>

    public class DataContext : DbContext
    {

        /// <summary>
        /// Added Code 03
        /// Creating the class constructor
        /// Pass the main class DataContext options to the base class with dependece injection
        /// Read more about dependence injection
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        /// <summary>
        /// Added Code 05
        /// Link each table as we did in the code below
        /// </summary>

        public DbSet<People> People { get; set; }

    }
}

// End Linking the model to the table
////////////////////////////////////////////////////////////////////////////////////////////////////////

// Start Get, Insert, update and delete record(s)
// "Slides 50"
// File: PeopleController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Added Code 01
/// Imports for the entity framework and other project classes 
/// </summary>

using Microsoft.EntityFrameworkCore;
using SQLApi.Data;
using SQLApi.Models;

namespace SQLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        /// <summary>
        /// Added Code 02
        /// Get all the records on the People table and return then as a list 
        /// </summary>
        [Route("")] // Uses the default route
        [HttpGet]
        public ActionResult<List<People>> GetDataFromTable([FromServices] DataContext context)
        {
            return context.People.ToList();
        }

        /// <summary>
        /// Added Code 03
        /// Get One record from the database 
        /// </summary>
        [Route("{id:int}")] // Uses the default route
        [HttpGet]
        public ActionResult<People> GetRecordById([FromServices] DataContext context, int id)
        {
            return context.People.Find(id);
        }

        /// <summary>
        /// Added Code 04
        /// Add new record to the database 
        /// </summary>
        [Route("")] // Uses the default route
        [HttpPost]
        public ActionResult<People> AddRecord([FromServices] DataContext context, [FromBody] People model)
        {
            if (ModelState.IsValid)
            {
                context.People.Add(model);
                context.SaveChanges();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Added Code 05
        /// Update one record to the database 
        /// </summary>
        [Route("")] // Uses the default route
        [HttpPut]
        public ActionResult<People> UpdateRecord([FromServices] DataContext context, [FromBody] People model)
        {
            if (ModelState.IsValid)
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Added Code 06
        /// Delete one record to the database 
        /// </summary>
        /// 

        [Route("{id:int}")] 
        [HttpDelete]
        public ActionResult DeleteRecord([FromServices] DataContext context, int id)
        {
                var model = context.People.Find(id);
                context.People.Remove(model);
                context.SaveChanges();
                return Ok();
        }

    }

}
// End Get, Insert, update and delete record(s)
////////////////////////////////////////////////////////////////////////////////////////////////////////