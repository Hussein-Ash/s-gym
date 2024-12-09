using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationBackend.Entities;

public class Set : BaseEntity<Guid>
{
    public string? Name { get; set; }

    public Guid? SetId { get; set; }
    [ForeignKey(nameof(SetId))]
    public Set? ExerciseSet { get; set; }
}
