using System;

namespace EvaluationBackend.Entities;

public class Message : BaseEntity<Guid>
{
    public string? SenderUsername { get; set; }
    public string? RecipientUsername { get; set; }
    public string? Content { get; set; }
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    public bool SenderDeleted { get; set; }
    public List<string>? Imgs { get; set; }
    public List<string>? VoiceMsgs { get; set; }

    //navi properities
    public Guid SenderId { get; set; }
    public AppUser? Sender { get; set; } 
    public Guid RecipientId { get; set; }
    public AppUser? Recipient { get; set; } 


}
