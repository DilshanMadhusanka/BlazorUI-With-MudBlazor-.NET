using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUI.Core.DTOs.Request
{
    public class TFAEnableRequestDTO
    {
        public string Password { get; set; }

        public bool IsEnable { get; set; }
    }
}
