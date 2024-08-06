using System.Net.Http.Json;
using BlazorApp1.Models.Notification;

namespace BlazorApp1.Services.Notifications;

public class NotificationsService : INotificationsService
{
    private const string UriRequest = "sample-data/notifications.json";
    private readonly HttpClient _htt;

    public NotificationsService(HttpClient htt)
    {
        _htt = htt;
    }

    public async Task<IEnumerable<NotificationModel>> GetNotifications()
    {
        var notifications = await _htt.GetFromJsonAsync<IEnumerable<NotificationModel>>(UriRequest);
        return notifications ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<NotificationModel>> GetActiveNotifications()
    {
        var notifications = await _htt.GetFromJsonAsync<IEnumerable<NotificationModel>>(UriRequest);
        var activeNotifications = (notifications ?? throw new InvalidOperationException()).Where(n => n.IsActive);
        return activeNotifications ?? throw new InvalidOperationException();
    }
}