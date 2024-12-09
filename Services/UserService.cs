using AutoMapper;
using e_parliament.Interface;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Entities;
using EvaluationBackend.Helpers.OneSignal;
using EvaluationBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Services
{
    public interface IUserService
    {
        Task<(UserDto? user, string? error)> Login(LoginForm loginForm);
        Task<(UserDto? user, string? error)> DeleteUser(Guid id, Guid userId);
        Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm);
        Task<(UserDto? UserDto, string? error)> AddAdmin(AddUserFromAdminForm registerForm);
        Task<(UserDto? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid userId);
        Task<(UserDto? user, string? error)> MyProfile(Guid id);
        Task<(string? message, string? error)> ChangeMyPassword(ChangePasswordForm form, Guid id);
        Task<(List<UserDto>? users, int? totalCount, string? error)> GetAll(UserFilter filter);
        Task<(UserDto? user, string? error)> GetUserById(Guid id);
        Task<(UserDto? user, string? error)> ChangeUserPassword(ChangePasswordForm form, Guid id, Guid userId);
        Task<(UserDto? user, string? error)> AdminUpdateUser(UpdateUserForm updateUserForm, Guid adminId, Guid userId);

        // Task<(string? user, string? error)> GetAccessToken(Guid? userId, DateTime? ExpierDate);
    }

    public class UserService : IUserService
    {

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;
        // private readonly IHubContext<ChatHubService> hubContext;

        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper, ITokenService tokenService
        , DataContext context
        // ,IHubContext<ChatHub> hubContext
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
            // this.hubContext = hubContext;
        }

        public async Task<(UserDto? user, string? error)> Login(LoginForm loginForm)
        {
            var user = await _repositoryWrapper.User.Get(u => u.UserName!.Trim().ToLower() == loginForm.UserName.Trim().ToLower());
            if (user == null || user.Deleted == true) return (null, "User not found");
            if (loginForm.Password != user.Password) return (null, "Wrong password");
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(userDto);
            return (userDto, null);
        }
        public async Task<(UserDto? user, string? error)> DeleteUser(Guid id, Guid userId)
        {
            var IsAdmin = await _repositoryWrapper.User.GetById(userId);
            if (!(IsAdmin!.Role == UserRole.Admin || IsAdmin.Role == UserRole.SuperAdmin || userId == id))
                return (null, "لايمكنك حذف المستخدم");

            var user = await _repositoryWrapper.User.GetById(id);
            if (user == null || user.Deleted == true) return (null, "User Not Found");


            var Deleting = await _repositoryWrapper.User.SoftDelete(id);
            if (Deleting == null) return (null, "Error Deleting");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);

        }
        public async Task<(UserDto? UserDto, string? error)> Register(RegisterForm registerForm)
        {
            var user = await _repositoryWrapper.User.Get(u => u.UserName!.Trim().ToLower() == registerForm.UserName!.Trim().ToLower());
            if (user != null) return (null, "User already exists");
            var newUser = new AppUser
            {
                UserName = registerForm.UserName!.Trim(),
                FullName = registerForm.FullName!.Trim(),
                Password = registerForm.Password,
                Img = registerForm.Img,
                Role = UserRole.User,
                PhoneNumber = registerForm.PhoneNumber!.Trim()

            };

            await _repositoryWrapper.User.CreateUser(newUser);
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(x => x.PhoneNumber == newUser.PhoneNumber!.Trim());
            if (sub != null)
            {
                sub.UserId = newUser.Id;
                sub.PlayerPhoto = newUser.Img;
                await _repositoryWrapper.Subscription.Update(sub);
                newUser.SubId = sub.Id;
                await _repositoryWrapper.User.Update(newUser);
                var noti = new Notification
                {
                    UserId = newUser.Id,
                    Title = "الاشتراك",
                    Body = $"تم انشاء اشتراك {sub.Type}"

                };
                await _context.Notifications.AddAsync(noti);
                if (await _context.SaveChangesAsync() <= 0) return (null, "error saving entity");
                OneSignal.SendNoitications(noti, newUser.FullName!);

            }
            var userDto = _mapper.Map<UserDto>(newUser);
            userDto.Token = _tokenService.CreateToken(userDto);
            return (userDto, null);
        }

        public async Task<(UserDto? UserDto, string? error)> AddAdmin(AddUserFromAdminForm AddUserForm)
        {
            var user = await _repositoryWrapper.User.Get(u => u.UserName!.Trim().ToLower() == AddUserForm.UserName!.Trim().ToLower());
            if (user != null) return (null, "الحساب موجود بالفعل");
            var newUser = new AppUser
            {
                UserName = AddUserForm.UserName,
                FullName = AddUserForm.FullName,
                Password = AddUserForm.Password,
                PhoneNumber = AddUserForm.PhoneNumber,
                Role = AddUserForm.Role,
                Img = AddUserForm.Img
            };
            await _repositoryWrapper.User.CreateUser(newUser);
            var userDto = _mapper.Map<UserDto>(newUser);
            userDto.Token = _tokenService.CreateToken(userDto);
            return (userDto, null);
        }



        public async Task<(UserDto? user, string? error)> UpdateUser(UpdateUserForm updateUserForm, Guid userId)
        {

            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null || user.Deleted == true) return (null, "User not found");

            if (user.UserName != updateUserForm.UserName && updateUserForm.UserName != null) user.UserName = updateUserForm.UserName;
            if (user.FullName != updateUserForm.FullName && updateUserForm.FullName != null) user.FullName = updateUserForm.FullName;
            if (user.PhoneNumber != updateUserForm.PhoneNumber && updateUserForm.PhoneNumber != null) user.PhoneNumber = updateUserForm.PhoneNumber;
            // if (user.Role != role) user.Role = role;
            // if (user.Password != updateUserForm.Password && user.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserForm.Password);

            await _repositoryWrapper.User.UpdateUser(user);

            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }

        public async Task<(UserDto? user, string? error)> UpdateUserRole(UpdateUserFromAdmin updateUserForm, Guid userId)
        {

            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null || user.Deleted == true) return (null, "User not found");

            if (user.Role != updateUserForm.Role && updateUserForm.Role != null) user.Role = (UserRole)updateUserForm.Role;

            await _repositoryWrapper.User.UpdateUser(user);

            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }

        public async Task<(string? message, string? error)> ChangeMyPassword(ChangePasswordForm form, Guid id)
        {
            var user = await _repositoryWrapper.User.GetById(id);
            if (form.NewPassword == user!.Password) return (null, "Same Password");
            user!.Password = form.NewPassword;

            var result = await _repositoryWrapper.User.Update(user);
            if (result == null) return (null, "Error Updating Password");

            return ("New Password Is Set", null);

        }

        public async Task<(UserDto? user, string? error)> GetUserById(Guid id)
        {
            var user = await _repositoryWrapper.User.Get(u => u.Id == id);
            if (user == null || user.Deleted == true) return (null, "User not found");
            var userDto = _mapper.Map<UserDto>(user);
            return (userDto, null);
        }
        public async Task<(List<UserDto>? users, int? totalCount, string? error)> GetAll(UserFilter filter)
        {

            var (users, totalCount) = await _repositoryWrapper.User.GetAll<UserDto>(
                x => x.Role != UserRole.Admin &&
                 x.Role != UserRole.SuperAdmin &&
                 (filter.FullName == null || x.FullName!.Contains(filter.FullName!)) &&
               (filter.UserName == null || x.UserName!.Contains(filter.UserName!)) &&
              (filter.PhoneNumber == null || x.PhoneNumber!.Contains(filter.PhoneNumber!)),

             filter.PageNumber, filter.PageSize
            );
            return (users, totalCount, null);
        }
        public async Task<(UserDto? user, string? error)> ChangeUserPassword(ChangePasswordForm form, Guid id, Guid userId)
        {
            var user = await _repositoryWrapper.User.GetById(userId);
            var admin = await _repositoryWrapper.User.GetById(id);
            if (admin == null) return (null, "something went wrong");

            if (form.NewPassword == user!.Password) return (null, "Same Password");

            user!.Password = form.NewPassword;
            var result = await _repositoryWrapper.User.Update(user);
            if (result == null) return (null, "Error Updating Password");

            return (_mapper.Map<UserDto>(user), null);

        }

        public async Task<(UserDto? user, string? error)> MyProfile(Guid id)
        {
            var user = await _repositoryWrapper.User.Get<UserDto>(x => x.Id == id);
            return user == null ? (null, "User not found") : (user, null);
        }
        public async Task<(UserDto? user, string? error)> AdminUpdateUser(UpdateUserForm updateUserForm, Guid adminId, Guid userId)
        {
            var admin = await _repositoryWrapper.User.GetById(adminId);
            if (admin == null) return (null, "something went wrong");

            var user = await _repositoryWrapper.User.Get(u => u.Id == userId);
            if (user == null || user.Deleted == true) return (null, "User not found");

            _mapper.Map(updateUserForm, user);
            // if (user.UserName != updateUserForm.UserName&& updateUserForm.UserName != null) user.UserName = updateUserForm.UserName;
            // if (user.FullName != updateUserForm.FullName && updateUserForm.FullName != null) user.FullName = updateUserForm.FullName;
            // if (user.PhoneNumber != updateUserForm.PhoneNumber && updateUserForm.PhoneNumber != null) user.PhoneNumber = updateUserForm.PhoneNumber;
            // if (user.Role != role) user.Role = role;
            // if (user.Password != updateUserForm.Password && user.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserForm.Password);

            var result = await _repositoryWrapper.User.UpdateUser(user);
            if (result == null) return (null, "Error Updating Password");

            var userDto = _mapper.Map<UserDto>(result);
            return (userDto, null);
        }

        // public async Task<(string? user, string? error)> GetAccessToken(Guid? userId, DateTime? ExpierDate)
        // {
        //     var user = await _repositoryWrapper.User.Get<UserDto>(x => x.Id == userId);

        //     if (ExpierDate < DateTime.UtcNow)
        //         return (null, "Expierd");

        //     var token = _tokenService.CreateToken(user!);
        //     return (token, null);
        // }
    }
}