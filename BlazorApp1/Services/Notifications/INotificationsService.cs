using BlazorApp1.Models.Notification;

namespace BlazorApp1.Services.Notifications;

public interface INotificationsService
{
    Task<IEnumerable<NotificationModel>> GetNotifications();
    Task<IEnumerable<NotificationModel>> GetActiveNotifications();
}