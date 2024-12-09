using System;

namespace EvaluationBackend.Entities;

public class Offer : BaseEntity<Guid>
{
    public string? Img { get; set; }

}
