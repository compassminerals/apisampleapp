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