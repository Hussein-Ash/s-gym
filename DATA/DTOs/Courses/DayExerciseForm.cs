using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayExerciseForm
{
    public required Guid MuscleId { get; set; }
    public required Guid ExerciseId { get; set; }
    public required Guid SetsId { get; set; }
    public bool Super { get; set; }
    public Guid? Exercise2Id { get; set; }
    public Guid? Sets2Id { get; set; }
}
