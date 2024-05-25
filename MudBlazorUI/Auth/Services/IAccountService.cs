
namespace MudBlazorUI.Auth.Services
{
    public interface IAccountService
    {
        Task<bool> ForgotPassword(string email);
    }
}