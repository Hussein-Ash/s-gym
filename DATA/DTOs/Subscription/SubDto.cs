using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class SubDto : BaseDto<Guid>
{
    public string? SectionName { get; set; }
    public string? User { get; set; }
    public string? CourseName { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? PlayerPhoto { get; set; }
    public string? PhoneNumber { get; set; }
    public SubInfoDto? SubInfo { get; set; }


}
