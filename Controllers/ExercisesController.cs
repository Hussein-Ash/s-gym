using EvaluationBackend.DATA.DTOs.Exercises;
using EvaluationBackend.Interface;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class ExercisesController : BaseController
    {
        private readonly IExerciseService _service;

        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Respons<ExerciseDto>>> GetAll([FromQuery] ExerciseFilter filter) => Ok(await _service.GetAll(filter), filter.PageNumber);


        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Add([FromBody] ExerciseForm form) => Ok(await _service.Add(form));



        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => OkObject(await _service.GetById(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await _service.Delete(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ExerciseUpdate Update, Guid id) => Ok(await _service.Update(Update, id));


    }
}
