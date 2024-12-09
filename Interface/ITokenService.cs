using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Entities;

namespace e_parliament.Interface
{
    public interface ITokenService
    {
        string CreateToken(UserDto user);
        string CreateRefreshToken(UserDto user);

    }
}