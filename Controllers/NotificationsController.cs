using EvaluationBackend.DATA.DTOs.Notifications;
using EvaluationBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers
{
    public class NotificationsController : BaseController
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Publisher,Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> AddCourse([FromBody] NotificationListForm form) => Ok(await _service.SendNotification(form,Id));

    }
}
