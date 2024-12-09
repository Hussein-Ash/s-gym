using System;

namespace EvaluationBackend.DATA.DTOs.Message;

public class MessageForm
{
    public required Guid RecipientId { get; set; }
    public string? Content { get; set; }
    public List<string>? Imgs { get; set; }
    public List<string>? VoiceMsgs { get; set; }


}
