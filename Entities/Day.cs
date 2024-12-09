using System;

namespace EvaluationBackend.Entities;

public class Day : BaseEntity<Guid>
{

    public int? DaySeq { get; set; }
    public ICollection<DayExercise>? Exercises { get; set; }
    public Guid? CourseId { get; set; }
    public Course? CourseName { get; set; }


}
