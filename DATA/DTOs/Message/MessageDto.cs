using System;

namespace EvaluationBackend.DATA.DTOs.Message;

public class MessageDto : BaseDto<Guid>
{
    public string? SenderUsername { get; set; }
    public string? RecipientUsername { get; set; }
    public string? Content { get; set; }
    public List<string>? Imgs { get; set; }
    public List<string>? VoiceMsgs { get; set; }
    public DateTime MessageSent { get; set; }
    public bool SenderDeleted { get; set; }




}
