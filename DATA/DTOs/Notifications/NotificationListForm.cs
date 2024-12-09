using System;

namespace EvaluationBackend.DATA.DTOs.Notifications;

public class NotificationListForm
{
    public ICollection<NotificationForm>? Notifications { get; set; }
}
