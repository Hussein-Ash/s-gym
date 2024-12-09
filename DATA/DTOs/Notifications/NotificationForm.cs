using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Notifications;

public class NotificationForm
{
    public Guid? ReseverId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

}
