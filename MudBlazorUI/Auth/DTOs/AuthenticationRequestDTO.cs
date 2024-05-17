using System.ComponentModel.DataAnnotations;

namespace MudBlazorUI.Auth.DTOs
{
    public class AuthenticationRequestDTO
    {


        public string UserName { get; set; } = "";


        public string Password { get; set; } = "";


    }
}
