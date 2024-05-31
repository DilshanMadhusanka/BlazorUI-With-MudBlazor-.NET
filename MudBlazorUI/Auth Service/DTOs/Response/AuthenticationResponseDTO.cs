
using System.ComponentModel.DataAnnotations;

namespace MudBlazorUI.Core.DTOs.Response
{
    public class AuthenticationResponseDTO
    {
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public bool? IsLocked { get; set; }
        public bool? EmailConfirmed { get; set; }

        public bool? Is2FAConfirmed { get; set; }
        public string Message { get; set; }





    }
}
