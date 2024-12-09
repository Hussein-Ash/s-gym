using System;
using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Entities;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationBackend.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet]
    public async Task<ActionResult<Respons<UserDto>>> GetAll([FromQuery] UserFilter filter) => Ok(await _service.GetAll(filter), filter.PageNumber);

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<UserDto>> MyProfile() => Ok(await _service.MyProfile(Id));

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(Guid id) => OkObject(await _service.GetUserById(id));

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public async Task<ActionResult> AddAdmin(AddUserFromAdminForm registerForm) => Ok(await _service.AddAdmin(registerForm));


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id) => Ok(await _service.DeleteUser(id, (Guid)Id!));

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdateUser(UpdateUserForm updateUserForm) => Ok(await _service.UpdateUser(updateUserForm, (Guid)Id!));

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("update-user/{id}")]
    public async Task<ActionResult> AdminUpdateUser(UpdateUserForm updateUserForm,Guid id) => Ok(await _service.AdminUpdateUser(updateUserForm, (Guid)Id!,id));


    [Authorize]
    [HttpPut("change-mypassword")]
    public async Task<ActionResult> ChangeMyPassword(ChangePasswordForm form) => Ok(await _service.ChangeMyPassword(form, (Guid)Id!));

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("change-user-password/{id}")]
     public async Task<ActionResult> ChangeUserPassword(ChangePasswordForm form, Guid id) => Ok(await _service.ChangeUserPassword(form, (Guid)Id!, id));




}

