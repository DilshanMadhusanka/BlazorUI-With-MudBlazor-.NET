
namespace MudBlazorUI.Notification_Service.DTOs
{
    public class NotificationResponseDTO : BaseEntity
    {
        public string Title { get; set; } 
        public string Content { get; set; } 
        public Guid? ReceiverId { get; set; }
        public bool ReadStatus { get; set; }
        public int Priority { get; set; }
        public string? Link { get; set; }
        public NotificationType Type { get; set; }


    }
}
