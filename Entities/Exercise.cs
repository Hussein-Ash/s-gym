using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationBackend.Entities;

public class Exercise : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? YouTubeLink { get; set; }
    public string? Img { get; set; }
    public Guid? MuscleId { get; set; }
    [ForeignKey(nameof(MuscleId))]
    public Muscle? MuscleName { get; set; }

    public Guid? ExerciseId { get; set; }
    [ForeignKey(nameof(ExerciseId))]
    public Exercise? Exercice { get; set; }


}
