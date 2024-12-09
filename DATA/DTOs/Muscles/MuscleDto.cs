using System;

namespace EvaluationBackend.DATA.DTOs.Muscles;

public class MuscleDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? Img { get; set; }

}
