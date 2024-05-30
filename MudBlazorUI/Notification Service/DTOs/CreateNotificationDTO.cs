


namespace MudBlazorUI.Notification_Service.DTOs
{
    public class CreateNotificationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid? ReceiverId { get; set; }
        public bool ReadStatus { get; set; } = false;
        public int Priority { get; set; } = 0;
        public string? Link { get; set; }
        public NotificationType Type { get; set; }
    }
}
