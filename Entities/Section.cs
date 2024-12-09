using System;

namespace EvaluationBackend.Entities;

public class Section : BaseEntity<Guid>
{
    public string? Name { get; set; }

    public ICollection<Course>? CourseName { get; set; }

    public ICollection<Subscription>? Subscriptions { get; set; }

}


