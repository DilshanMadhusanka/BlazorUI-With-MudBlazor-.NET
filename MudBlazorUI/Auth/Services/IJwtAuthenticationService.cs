
namespace MudBlazorUI.Auth.DTOs
{
    public interface IJwtAuthenticationService
    {
        ValueTask<string> GetJwtAsync();
        Task<string> LoginAsync(AuthenticationRequestDTO request);
        Task LogoutAcync();

        Task<UserModelResponseDTO?> GetUserDetails();

        public Task<bool> Refresh();
    }
}