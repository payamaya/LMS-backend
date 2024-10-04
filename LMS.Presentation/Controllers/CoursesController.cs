
using LMS.Infrastructure.Dtos;
using LMS.Persistance;
using LMS.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceManager _sm;

        public CoursesController(ApplicationDbContext context, IServiceManager sm)
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
        //[Authorize(Roles = "Teacher")]
        //[Authorize(Roles = "Student")]
        //[Authorize(Roles = "Teacher")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            return Ok(await _sm.CourseService.GetCoursesAsync());
            /*return await _context.Courses.ToListAsync();*/
        }

        // GET: api/Courses/5
        /// <summary>
        /// Hur gör man det här, 3 * /
        /// </summary>
        /// <returns></returns>
        [HttpGet("Student")]
        //[Authorize]
        //[Authorize(Roles = "Teacher")]
        //[OverrideAuthorization]
        //[Authorize(Roles = "Student")]
        [Produces("application/json")]
        public async Task<ActionResult<CourseDetailedDto>> GetCourse()
        {
            var course = await _sm.CourseService.GetCourseAsync(User);

            return Ok(course);
        }

        // GET: api/Courses/5
        /// <summary>
        /// Hur gör man det här, 3 * /
        /// </summary>
        /// <param name="id">Hej hej</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        //[Authorize]
        //[Authorize(Roles = "Teacher")]
        //[Authorize(Roles = "Student")]
        //[Authorize(Roles = "Teacher")]
        [Produces("application/json")]
        public async Task<ActionResult<CourseDetailedDto>> GetCourse(Guid id)
        {
            var course = await _sm.CourseService.GetCourseAsync(id);

            return Ok(course);
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<CourseDto>> PostCourse(CoursePostDto postDto)
        {
            CourseDto newCourseDto = await _sm.CourseService.PostCourseAsync(postDto);

            return CreatedAtAction(nameof(GetCourse), new { id = newCourseDto.Id }, newCourseDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteCourseAsync(Guid id)
        {
            await _sm.CourseService.DeleteCourseAsync(id);
            return NoContent();
        }

    }
}
