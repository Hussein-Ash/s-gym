using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.Courses;

public class CourseDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public Guid? SectionId { get; set; }
    public string? SectionName { get; set; }
    public int? DayCount { get; set; }
    public ICollection<DayDto>? Days { get; set; }
    

}
