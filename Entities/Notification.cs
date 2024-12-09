using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationBackend.Entities;

public class Notification : BaseEntity<Guid>
{
    public Guid? SenderId { get; set; }
    public AppUser? Sender { get; set; }

    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }

    public string? Title { get; set; }
    public string? Body { get; set; }


    public Guid? RefrenceId { get; set; }
    public MessageTypeEnum Type { get; set; }

}
public enum MessageTypeEnum
{
    Problem
}