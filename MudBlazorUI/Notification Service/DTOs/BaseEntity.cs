
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MudBlazorUI.Notification_Service.DTOs
{
    public class BaseEntity
    {

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } //Delete or not
    }


    public enum NotificationType
    {
        Success, //green
        Error, //red
        Warning, //yellow
        Info //blue
    }
}
