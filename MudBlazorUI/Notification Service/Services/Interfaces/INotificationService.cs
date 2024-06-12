using MudBlazorUI.Notification_Service.DTOs;

namespace MudBlazorUI.Notification_Service.Services
{
    public interface INotificationService
    {
        public Task<bool> MarkAllAsRead(Guid userId);
        public Task<bool> MarkAsRead(Guid NotificationId);
        Task<IEnumerable<NotificationResponseDTO>> GetAllNotifications(NotificationRequestDTO notificationRequestDTO);
    }
}