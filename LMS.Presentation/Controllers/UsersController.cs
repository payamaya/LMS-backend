//using System;
//using System.Collections.Generic;
//using System.Linq;

using LMS.Infrastructure.Dtos;
using LMS.Persistance;
using LMS.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers
{
    [Route("api/users")]
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

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityUser(Guid id)
        {
            await _sm.UserService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
