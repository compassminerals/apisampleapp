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
