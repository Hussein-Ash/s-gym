using EvaluationBackend.DATA.DTOs.Message;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{

    public class MessagesController : BaseController
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        // [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<Respons<MessageDto>>> GetAll([FromQuery] MessageFilter filter) => Ok(await _service.GetMessagesForUser(filter), filter.PageNumber);


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MessageDto>> Add([FromBody] MessageForm form) => Ok(await _service.AddMessage(form,Id));

        [HttpGet("thread/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(Guid userId) => Ok(await _service.GetMessageThread(Id,userId));

        // // [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        // [HttpGet("{id}")]
        // public async Task<ActionResult> Get(Guid id) => OkObject(await _service.GetById(id));

        [Authorize]
        [HttpDelete("{mesgid}")]
        public async Task<ActionResult> Delete(Guid mesgid) => Ok(await _service.DeleteMessage(mesgid,Id));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(MessageUpdate Update, Guid id) => Ok(await _service.UpdateMessage(Update, id,Id));



    }
}
