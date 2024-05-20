

using System.ComponentModel.DataAnnotations;

namespace MudBlazorUI.Core.DTOs.Request
{
    public class AuthenticationRequestDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
