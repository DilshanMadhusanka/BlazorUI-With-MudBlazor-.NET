
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth.DTOs
{
    public interface IJwtAuthenticationService
    {
        ValueTask<string> GetJwtAsync();
        Task<AuthenticationResponseDTO> LoginAsync(AuthenticationRequestDTO request);
        Task LogoutAcync();

        Task<UserModelResponseDTO?> GetUserDetails();

        public Task<bool> Refresh();
    }
}