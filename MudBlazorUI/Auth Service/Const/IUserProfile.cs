
namespace MudBlazorUI.Auth.Const
{
    public interface IUserProfile
    {
        Task<string?> GetTheme();
        Task SetDarkTheme(string theme);
    }
}