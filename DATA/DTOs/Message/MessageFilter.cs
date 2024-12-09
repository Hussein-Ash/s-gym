using System;
using OneSignalApi.Model;

namespace EvaluationBackend.DATA.DTOs.Message;

public class MessageFilter : BaseFilter
{
    public string? Username { get; set; }

}
