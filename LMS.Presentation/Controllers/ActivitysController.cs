using LMS.Application.Exceptions;
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

        // GET: api/Activities
        /// <summary>
        /// Retrieves a list of all activities.
        /// </summary>
        /// <returns>A list of <see cref="ActivityDto"/> objects.</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            return Ok(await _sm.ActivityService.GetActivitiesAsync());
        }

        // GET: api/Activities/5
        /// <summary>
        /// Retrieves a specific activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity to retrieve.</param>
        /// <returns>An <see cref="ActivityDto"/> object if found; otherwise, a 404 Not Found result.</returns>
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

        // PUT: api/Activities/5
        /// <summary>
        /// Updates an existing activity identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity to update.</param>
        /// <param name="activity">The updated <see cref="Activity"/> object.</param>
        /// <returns>A 204 No Content result if the update is successful; otherwise, a 400 Bad Request or 404 Not Found result.</returns>
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

        // POST: api/Activities
        /// <summary>
        /// Creates a new activity.
        /// </summary>
        /// <param name="postDto">PostDto is required here</param>
        /// <param name="activity">The <see cref="Activity"/> object to create.</param>
        /// <returns>A 201 Created result with the location of the newly created activity; otherwise, a 400 Bad Request.</returns>
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(ActivityPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActivityDto? getActivityDto = await _sm.ActivityService.PostActivityAsync(postDto);
                return CreatedAtAction(nameof(GetActivity), new { id = getActivityDto?.Id }, getActivityDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred"); // General error handling
            }
        }

        // DELETE: api/Activities/5
        /// <summary>
        /// Deletes an existing activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity to delete.</param>
        /// <returns>A 204 No Content result if the deletion is successful; otherwise, a 404 Not Found result.</returns>
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
