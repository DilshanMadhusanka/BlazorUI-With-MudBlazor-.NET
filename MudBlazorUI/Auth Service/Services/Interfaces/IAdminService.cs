using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth_Service.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserModelResponseDTO>?> GetAllUsersDetaiils(string searchString);
    }
}