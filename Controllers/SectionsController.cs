using EvaluationBackend.DATA.DTOs.Sections;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class SectionsController : BaseController
    {

        private readonly ISectionsService _service;

        public SectionsController(ISectionsService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<Respons<SectionDto>>> GetAll([FromQuery] SectionFilter filter) => Ok(await _service.GetAll(filter), filter.PageNumber);


        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<SectionDto>> Add([FromBody] SectionForm form) => Ok(await _service.Add(form));



        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => OkObject(await _service.GetById(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await _service.Delete(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(SectionUpdate Update, Guid id) => Ok(await _service.Update(Update, id));

    }
}
