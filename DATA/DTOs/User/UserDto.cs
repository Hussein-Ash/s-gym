using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public string? Img { get; set; }


    }


}