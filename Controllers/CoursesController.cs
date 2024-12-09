using EvaluationBackend.DATA.DTOs.Courses;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class CoursesController : BaseController
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Respons<CourseDto>>> GetAllCourses([FromQuery] CourseFilter filter) => Ok(await _service.GetAllCourses(filter), filter.PageNumber);


        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseForm form) => Ok(await _service.AddCourse(form));



        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseById(Guid id) => OkObject(await _service.GetCourseById(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(Guid id) => Ok(await _service.DeleteCourse(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("day/{id}")]
        public async Task<ActionResult> DeleteDay(Guid id) => Ok(await _service.DeleteCourse(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("day/exercise/{id}")]
        public async Task<ActionResult> DeleteExercise(Guid id) => Ok(await _service.DeleteCourse(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(CourseUpdate Update, Guid id) => Ok(await _service.UpdateCourse(Update, id));



    }
}
