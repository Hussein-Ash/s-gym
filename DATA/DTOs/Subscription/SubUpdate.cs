using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class SubUpdate
{
    public Guid? SectionId { get; set; }
    public SubType? Type { get; set; }

}
