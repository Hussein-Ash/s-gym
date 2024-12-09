using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayDto : BaseDto<Guid>
{
    public int? DaySeq { get; set; }
    public ICollection<DayExerciseDto>? Exercises { get; set; }

}
