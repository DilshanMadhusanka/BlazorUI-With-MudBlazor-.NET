
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth.Const
{
    public interface IUserProfile
    {
        Task<string?> GetTheme();
        Task SetDarkTheme(string theme);
        public Task<UserModelResponseDTO?> GetUserProfile();
        public  Task SetUserProfile(UserModelResponseDTO userModelResponseDTO);
        public  Task SetRememberMe(AuthenticationRequestDTO authenticationRequestDTO);
        public Task<AuthenticationRequestDTO?> GetRememberMe();
    }
}