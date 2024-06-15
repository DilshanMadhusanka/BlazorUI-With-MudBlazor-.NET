
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth.Const
{
    public interface IUserProfile
    {
        Task<string?> GetTheme();
        Task SetDarkTheme(string theme);
        public Task<UserModelResponseDTO?> GetUserProfile();

        public  Task SetUserProfile(UserModelResponseDTO userModelResponseDTO);
    }
}