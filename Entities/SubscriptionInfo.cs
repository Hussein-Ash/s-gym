using System;

namespace EvaluationBackend.Entities;

public class SubscriptionInfo : BaseEntity<Guid>
{
    public string? Goal { get; set; }
    public string? UnWantedFood { get; set; }
    public string? Injurse { get; set; }
    public int? Age { get; set; }
    public string? Height { get; set; }
    public string? Weight { get; set; }
    public string? Work { get; set; }
    public string? Sleep { get; set; }
    public bool UseHrmon { get; set; }
    public string? Hrmon { get; set; }
    public string? Notes { get; set; }
    public List<string>? Imges { get; set; }
    public string? Since { get; set; }
    public string? Tests { get; set; }
    public string? GymName { get; set; }
    public string? Name { get; set; }
    public string? GymAddress { get; set; }


    public Guid SubId { get; set; }
    public Subscription? Sub { get; set; }



}
