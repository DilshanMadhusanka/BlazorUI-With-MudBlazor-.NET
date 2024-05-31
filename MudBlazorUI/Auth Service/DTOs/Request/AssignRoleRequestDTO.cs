using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUI.Core.DTOs.Request
{
    public class AssignRoleRequestDTO
    {

        public string Role { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;
    }
}
