using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class SubForm
{
    public Guid? UserId { get; set; }
    public required Guid SectionId { get; set; }
    public string? PhoneNumber { get; set; }
    public SubType Type { get; set; }

}
