using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationBackend.Entities;

public class Subscription : BaseEntity<Guid>
{
    public Guid? SectionId { get; set; }
    public Section? SectionName { get; set; }

    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }

    public Guid? CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Course? CourseName { get; set; }

    public Guid? SubInfoId { get; set; }
    public SubscriptionInfo? SubInfo { get; set; }

    public SubType? Type { get; set; }
    public PlayerStatus? Status { get; set; }
    public string? PlayerPhoto { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? ResgisterDate { get; set; }




}


public enum SubType
{
    Bronze,
    Silver,
    Gold
}
public enum PlayerStatus
{
    New,
    NeedCourse,
    Finished
}
