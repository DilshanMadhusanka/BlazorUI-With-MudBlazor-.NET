using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUI.Core.DTOs.Request
{
    public class ChangePasswordRequestDTO
    {
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }
}
