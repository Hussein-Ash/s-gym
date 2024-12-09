using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayUpdate
{
    // public int? DaySeq { get; set; }
    public Guid? Id { get; set; }
    public int? DaySeq { get; set; }
    public ICollection<DayExerciseUpdate>? Exercises { get; set; }


}
