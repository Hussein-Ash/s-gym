using System;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Notifications;
using EvaluationBackend.Entities;
using EvaluationBackend.Helpers.OneSignal;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Services;
public interface INotificationService
{
    Task<(string? message, string? error)> SendNotification(NotificationListForm notification, Guid id);

}
public class NotificationService : INotificationService
{
    private readonly DataContext _context;
    public NotificationService(DataContext context)
    {
        _context = context;
    }

    public async Task<(string? message, string? error)> SendNotification(NotificationListForm notification, Guid id)
    {
        if(notification.Notifications == null ) return (null,"you send empty stuff");
        foreach (var noti in notification.Notifications)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == noti.ReseverId);
            if(user == null) return (null,$"user not found his id is {noti.ReseverId}");
            var newNotification = new Notification
            {
                SenderId = id,
                UserId = noti.ReseverId,
                Title = noti.Title,
                Body = noti.Body
            };
            await _context.Notifications.AddAsync(newNotification);
            if(await _context.SaveChangesAsync()<= 0 ) return (null,"error saving");
            var result = OneSignal.SendNoitications(newNotification,user.FullName!);
            if(!result) return(null,"error sending");
            
            
        }
        return("Notifications Sent",null);
        
    }
}
