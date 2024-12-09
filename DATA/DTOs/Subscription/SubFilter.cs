using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class SubFilter : BaseFilter
{
    public string? PhoneNumber { get; set; }
    public SubType? Type { get; set; }
    public string? SectionName { get; set; }
}
