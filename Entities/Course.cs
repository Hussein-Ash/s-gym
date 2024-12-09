using System;

namespace EvaluationBackend.Entities;

public class Course : BaseEntity<Guid>
{
    public string? Name { get; set; }

    public Guid? SectionId { get; set; }
    public Section? SectionName { get; set; }
    public int? DayCount { get; set; }

    public ICollection<Day>? Days { get; set; }
    public ICollection<Subscription>? Subs { get; set; }

}
