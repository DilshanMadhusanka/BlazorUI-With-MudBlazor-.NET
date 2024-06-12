using MudBlazorUI.Core.DTOs.Response;
using MudBlazorUI.Notification_Service.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MudBlazorUI.Notification_Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpClientFactory _factory;

        public NotificationService(IHttpClientFactory factory)
        {
            _factory = factory;
        }


        public async Task<IEnumerable<NotificationResponseDTO>?> GetAllNotifications(NotificationRequestDTO notificationRequestDTO)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("ApiGateWay/Notification-Api/Notification/All-Notifications", notificationRequestDTO);

            var content = await result.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<IEnumerable<NotificationResponseDTO>>(content);

        
            return response;

        }

        public async Task<bool> MarkAsRead(Guid NotificationId)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("ApiGateWay/Notification-Api/Notification/Mark-as-Read", NotificationId);

            if (result.IsSuccessStatusCode)
            {
                return true;

            }
            return false;

            
        }

        public async Task<bool> MarkAllAsRead(Guid userId)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("ApiGateWay/Notification-Api/Notification/MarkAll-as-Read", userId);

            if (result.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }







    }
}
