using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationBackend.Entities;

public class DayExercise : BaseEntity<Guid>
{
    public Guid? MuscleId { get; set; }
    [ForeignKey(nameof(MuscleId))]
    public Muscle? MuscleName { get; set; }

    public Guid? ExerciseId { get; set; }
    [ForeignKey(nameof(ExerciseId))]
    public Exercise? ExerciseName { get; set; }

    public Guid? SetsId { get; set; }
    [ForeignKey(nameof(SetsId))]
    public Set? Sets { get; set; }
    

    public bool Super { get; set; } = false;

    public Guid DayId { get; set; }
    [ForeignKey(nameof(DayId))]
    public Day? Day { get; set; }

}
