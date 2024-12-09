using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RefreshToken { get; set; }
        public string? Img { get; set; }
        public UserRole Role { get; set; }

        public Guid? SubId { get; set; }
        public Subscription? Sub { get; set; }
        public ICollection<Notification>? NotificationsSent { get; set; }
        public ICollection<Notification>? NotificationsReceived { get; set; }

        public ICollection<Message>? MessagesSent { get; set; }
        public ICollection<Message>? MessagesReceived { get; set; }

    }

    public enum UserRole
    {
        SuperAdmin,
        Admin,
        Publisher,
        User
    }

}