using MudBlazorUI.Auth_Service.DTOs.Request;
using MudBlazorUI.Auth_Service.DTOs.Response;
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;

namespace MudBlazorUI.Auth_Service.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserModelResponseDTO>?> GetAllUsersDetaiils(string searchString);
        public Task<bool> Update(UpdateUserRequest updateUserRequest);
        public Task<HttpResponseMessage> GetUserLockedStatus(string id);
        public Task<IEnumerable<UserLoginDeviceInfoResponse>?> GetAllLoginDetails(string searchString);
        public  Task<HttpResponseMessage> GetUserRole(string id);
        public Task<HttpResponseMessage> UpdateRole(AssignRoleRequestDTO assignRoleRequestDTO);
    }
}