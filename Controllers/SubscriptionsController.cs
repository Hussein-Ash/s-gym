using EvaluationBackend.DATA.DTOs.Subscription;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class SubscriptionsController : BaseController
    {
        private readonly ISubscriptionService _service;

        public SubscriptionsController(ISubscriptionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<Respons<SubDto>>> GetAll([FromQuery] SubFilter filter) => Ok(await _service.GetAll(filter), filter.PageNumber);


        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<SubDto>> Add([FromBody] SubForm form) => Ok(await _service.Add(form));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{SubId}/info")]
        public async Task<ActionResult<SubDto>> fill([FromBody] SubInfoForm form, Guid SubId) => Ok(await _service.Fill(form, SubId));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{SubId}/course")]
        public async Task<ActionResult<SubDto>> AddCourse([FromBody] SubCourseForm form, Guid SubId) => Ok(await _service.AddCourse(form, SubId));


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => OkObject(await _service.GetById(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await _service.Delete(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(SubUpdate Update, Guid id) => Ok(await _service.Update(Update, id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("multi-delete")]
        public async Task<ActionResult> MultiDelete(MultDelete multDelete) => Ok(await _service.MultiDelete(multDelete));


    }
}
