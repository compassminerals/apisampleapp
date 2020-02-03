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
