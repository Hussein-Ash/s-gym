using EvaluationBackend.DATA.DTOs.Offer;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class OffersController : BaseController
    {
        private readonly IOfferService _service;

        public OffersController(IOfferService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Respons<OfferDto>>> GetAll([FromQuery] OfferFilter filter) => Ok(await _service.GetAll(filter), filter.PageNumber);


        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<OfferDto>> Add([FromBody] OfferForm form) => Ok(await _service.Add(form));



        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => OkObject(await _service.GetById(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await _service.Delete(id));

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(OfferUpdate Update, Guid id) => Ok(await _service.Update(Update, id));

    }
}
