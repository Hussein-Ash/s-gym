using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneSignalApi.Model;

namespace EvaluationBackend.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

      
        /// <remarks>
        /// Token: <code>eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwZjhmOGE3MS1mYTkzLTQ4OTctN2E1NC00NWEwNjk2MTljMGUiLCJpZCI6IjBmOGY4YTcxLWZhOTMtNDg5Ny03YTU0LTQ1YTA2OTYxOWMwZSIsIlJvbGUiOiJTdXBlckFkbWluIiwiRXhwaWVyRGF0ZSI6IjEyLzkvMjAyNCAzOjQzOjEwIFBNIiwibmJmIjoxNzMzNzU4MDkwLCJleHAiOjE3MzQzNjI4OTAsImlhdCI6MTczMzc1ODA5MH0.jInM46fK7-hK3bH6Rzk7i7DPGrvGtklyeq71y5hwZPMh2PXzeqC5tUWl3MPl6AbtQbQu9Rf5l5u4mg3-ZWc7rQ</code>
        /// </remarks>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginForm loginForm) => Ok(await _userService.Login(loginForm));

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterForm registerForm) => Ok(await _userService.Register(registerForm));

        // [Authorize]
        // [HttpGet("AccessToken")]
        // public async Task<ActionResult> GetAccessToken() => Ok(await _userService.GetAccessToken(Id, TokenExpier));

    }
}