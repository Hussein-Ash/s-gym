using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayForm
{

    public int DaySeq { get; set; }
    public required List<DayExerciseForm> Exercises { get; set; }


}
