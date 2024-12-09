using System;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class CourseUpdate
{
    public string? Name { get; set; }
    public Guid? SectionId { get; set; }
    public ICollection<DayUpdate>? Days { get; set; }


}
