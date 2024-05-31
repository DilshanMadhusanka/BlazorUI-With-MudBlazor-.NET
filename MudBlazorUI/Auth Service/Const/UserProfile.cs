using Blazored.LocalStorage;

namespace MudBlazorUI.Auth.Const
{
    public class UserProfile : IUserProfile
    {
        private readonly ILocalStorageService _localStorageService;

        //const
        private const string THEME = nameof(THEME);

        public UserProfile(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task SetDarkTheme(string theme)
        {
            await _localStorageService.SetItemAsStringAsync(THEME, theme);
        }

        public async Task<string?> GetTheme()
        {
            return await _localStorageService.GetItemAsStringAsync(THEME);

        }


    }
}
