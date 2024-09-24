//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Http;

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using LMS.API;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Persistance;
using LMS.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceManager _sm;

        public ActivitiesController(ApplicationDbContext context, IServiceManager sm)
        {
            _context = context;
            _sm = sm;
        }

        // GET: api/Courses
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            return Ok(await _sm.ActivityService.GetActivitiesAsync());
            /*return await _context.Courses.ToListAsync();*/
        }

        // GET: api/Courses/5
        /// <summary>
        /// Hur gör man det här, 3 * /
        /// </summary>
        /// <param name="id">Hej hej</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        public async Task<ActionResult<ActivityDto>> GetActivity(Guid id)
        {
            var activity = await _sm.ActivityService.GetActivityAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
             [HttpPut("{id}")]
             public async Task<IActionResult> PutActivity(Guid id, Activity activity)
             {
                 if (id != activity.Id)
                 {
                     return BadRequest();
                 }

                 _context.Entry(activity).State = EntityState.Modified;

                 try
                 {
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ActivityExists(id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }

                 return NoContent();
             }

     

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityAsync(Guid id)
        {
            await _sm.ActivityService.DeleteActivityAsync(id);
            return NoContent();
        }




        private bool ActivityExists(Guid id)
        {
            return _context.Activitys.Any(a => a.Id == id);
        }

    }
}

