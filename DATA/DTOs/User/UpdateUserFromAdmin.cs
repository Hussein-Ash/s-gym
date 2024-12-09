using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.DATA.DTOs.User;

public class UpdateUserFromAdmin
{
    public UserRole? Role { get; set; }
}
