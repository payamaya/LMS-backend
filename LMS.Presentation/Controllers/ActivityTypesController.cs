
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Persistance;
using LMS.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers
{
    [Route("api/activityTypes")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceManager _sm;

        public ActivityTypesController(ApplicationDbContext context, IServiceManager sm)
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
        public async Task<ActionResult<IEnumerable<ActivityTypeDto>>> GetActivityTypes()
        {
            return Ok(await _sm.ActivityTypeService.GetActivityTypesAsync());
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
        public async Task<ActionResult<ActivityTypeDto>> GetActivityType(Guid id)
        {
            var activityType = await _sm.ActivityTypeService.GetActivityTypeAsync(id);

            if (activityType == null)
            {
                return NotFound();
            }

            return Ok(activityType);
        }

    }
}
