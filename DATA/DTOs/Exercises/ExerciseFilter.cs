using System;

namespace EvaluationBackend.DATA.DTOs.Exercises;

public class ExerciseFilter : BaseFilter
{
    public string? Name { get; set; }
    public Guid? MuscleId { get; set; }
    public string? MuscleName { get; set; }
    

}
