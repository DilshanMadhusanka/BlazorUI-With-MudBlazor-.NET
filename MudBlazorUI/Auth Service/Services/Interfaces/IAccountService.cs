
using MudBlazorUI.Auth_Service.DTOs.Request;
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth.Services
{
    public interface IAccountService
    {
        public Task<bool> ForgotPassword(string email);
        public Task<UserModelResponseDTO?> GetUserDetaiils();
        public  Task<string?> Resend2FACode(string email);
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordRequestDTO changePassword);
        public Task<HttpResponseMessage> Enable2FA(TFAEnableRequestDTO tFAEnableRequestDTO);
        public Task<string?> GetProfileImage(string fileName);
        public Task<HttpResponseMessage> UploadProfileImage(ImageUploadRequestDTO imageUploadRequestDTO);
    }
}