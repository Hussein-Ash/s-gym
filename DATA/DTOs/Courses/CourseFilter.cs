using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class CourseFilter : BaseFilter
{
    public Guid? SectionId { get; set; }
    public Guid? MuscleId { get; set; }
    public Guid? ExerciseId { get; set; }

}
