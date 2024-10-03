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

namespace LMS.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Teacher")]
	public class UsersController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IServiceManager _sm;

		public UsersController(ApplicationDbContext context, IServiceManager sm)
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
		public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(bool onlyTeachers = false)
		{
			return Ok(await _sm.UserService.GetUsersAsync(onlyTeachers));
		}

		// GET: api/Courses/5
		/// <summary>
		/// Hur gör man det här, 3 * /
		/// </summary>
		/// <param name="id">Hej hej</param>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		[Produces("application/json")]
		public async Task<ActionResult<UserDto>> GetUser(Guid id)
		{
			var user = await _sm.UserService.GetUserAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		// PUT: api/Courses/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		/*     [HttpPut("{id}")]
             public async Task<IActionResult> PutCourse(int id, Course course)
             {
                 if (id != course.Id)
                 {
                     return BadRequest();
                 }

                 _context.Entry(course).State = EntityState.Modified;

                 try
                 {
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!CourseExists(id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }

                 return NoContent();
             }*/

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
		public async Task<IActionResult> DeleteActivityUser(Guid id)
		{
			await _sm.UserService.DeleteUserAsync(id);

			return NoContent();
		}

		/*private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }*/
	}
}
