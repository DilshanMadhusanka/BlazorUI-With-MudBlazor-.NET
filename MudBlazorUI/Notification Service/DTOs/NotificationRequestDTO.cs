using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUI.Notification_Service.DTOs
{
    public class NotificationRequestDTO
    {
        public Guid ReceiverId { get; set; }
        public string SearchString { get; set; } = string.Empty;

    }
}
