using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class DayExerciseUpdate
{
    public Guid? Id { get; set; }
    public Guid? MuscleId { get; set; }
    public Guid? ExerciseId { get; set; }
    public Guid? SetsId { get; set; }
    public bool? Super { get; set; }
    public Guid? Exercise2Id { get; set; }
    public Guid? Sets2Id { get; set; }

}
