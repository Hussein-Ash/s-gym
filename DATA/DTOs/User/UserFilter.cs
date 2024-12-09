using EvaluationBackend.DATA.DTOs;
using EvaluationBackend.Entities;

namespace EvaluationBackend;

public class UserFilter : BaseFilter
{
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }

    
}
