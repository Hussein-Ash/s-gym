using System.ComponentModel.DataAnnotations;
namespace EvaluationBackend.DATA.DTOs.User
{
    public class LoginForm
    {
        public required String UserName { get; set; }
        public required String Password { get; set; }
    }
}