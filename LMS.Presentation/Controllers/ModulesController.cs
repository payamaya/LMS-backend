//using System;
//using System.Collections.Generic;

using LMS.Application.Exceptions;
using LMS.Infrastructure.Dtos;
using LMS.Persistance;
using LMS.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers
{
    [Route("api/modules")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class ModulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceManager _sm;

        public ModulesController(ApplicationDbContext context, IServiceManager sm)
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
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
        {
            return Ok(await _sm.ModuleService.GetModulesAsync());
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
        public async Task<ActionResult<ModuleDto>> GetModule(Guid id)
        {
            var module = await _sm.ModuleService.GetModuleAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return Ok(module);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModulePostDto modulePostDto)
        {

            try
            {
                ModuleDto? newModuleDto = await _sm.ModuleService.PostModuleAsync(modulePostDto);
                return CreatedAtAction(nameof(GetModule), new { id = newModuleDto.Id }, newModuleDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); // Return 404 if course not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred"); // General error handling
            }
        }
        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityModule(Guid id)
        {
            await _sm.ModuleService.DeleteModuleAsync(id);

            return NoContent();
        }

    }
}
