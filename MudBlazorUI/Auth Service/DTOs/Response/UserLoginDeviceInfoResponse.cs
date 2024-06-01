namespace MudBlazorUI.Auth_Service.DTOs.Response
{
    public class UserLoginDeviceInfoResponse
    {
        public Guid UserId { get; set; }

        public string? UserAgentDetails { get; set; }

        public DateTime LoginDate { get; set; } = DateTime.Now;

        public string? IP { get; set; }

        public int Status { get; set; }


    }
}
