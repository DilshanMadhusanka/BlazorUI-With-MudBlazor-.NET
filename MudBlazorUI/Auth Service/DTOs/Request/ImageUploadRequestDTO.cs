using Microsoft.AspNetCore.Http;

namespace MudBlazorUI.Auth_Service.DTOs.Request
{
    public class ImageUploadRequestDTO
    {

        public IFormFile File { get; set; }

        public string UserId { get; set; }
    }
}
