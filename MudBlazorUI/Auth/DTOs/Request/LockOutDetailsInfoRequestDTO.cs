
using System.ComponentModel.DataAnnotations;


namespace MudBlazorUI.Core.DTOs.Request
{
    public class LockOutDetailsInfoRequestDTO
    {
        [Required]
        public string Email { get; set; }
        public bool LockoutEnable { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }

        //if this is flsse , locked user is unlocked
        public bool? LockUser { get; set; }


    }
}
