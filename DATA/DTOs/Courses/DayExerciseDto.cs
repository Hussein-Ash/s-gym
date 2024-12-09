using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayExerciseDto : BaseDto<Guid>
{
    public Guid? MuscleId { get; set; }
    public string? Muscle { get; set; }
    public Guid? ExerciseId { get; set; }
    public string? Exercise { get; set; }
    public Guid? SetsId { get; set; }
    public string? Sets { get; set; }
    public bool? Super { get; set; }
    public Guid? Exercise2Id { get; set; }
    public string? Exercise2 { get; set; }
    public Guid? Sets2Id { get; set; }
    public string? Sets2 { get; set; }
}
