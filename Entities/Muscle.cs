using System;

namespace EvaluationBackend.Entities;

public class Muscle : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Img { get; set; }

    public Guid? ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }

    public Guid? DayId { get; set; }
    public Day? DayName { get; set; }

}
