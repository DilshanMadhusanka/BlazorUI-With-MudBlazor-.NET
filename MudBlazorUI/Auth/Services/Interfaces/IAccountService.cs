
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth.Services
{
    public interface IAccountService
    {
        public Task<bool> ForgotPassword(string email);
        public Task<UserModelResponseDTO?> GetUserDetaiils();
        public  Task<string?> Resend2FACode(string email);
    }
}