using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class CourseForm
{
    public required string Name { get; set; }
    public required Guid SectionId { get; set; }
    public required List<DayForm> Days { get; set; }

}
