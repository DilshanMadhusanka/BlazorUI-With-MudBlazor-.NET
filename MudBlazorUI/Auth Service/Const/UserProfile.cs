using Blazored.LocalStorage;
using MudBlazorUI.Core.DTOs.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System;
using System.Threading.Tasks;
using MudBlazorUI.Core.DTOs.Request;

namespace MudBlazorUI.Auth.Const
{
    public class UserProfile : IUserProfile
    {
        private readonly ILocalStorageService _localStorageService;

        private const string THEME = nameof(THEME);
        private const string USERPROFILE = nameof(USERPROFILE);
        private const string R3M3M4M3 = nameof(R3M3M4M3);

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


        public async Task SetUserProfile(UserModelResponseDTO userModelResponseDTO)
        {
            await SetSecureItem(USERPROFILE, userModelResponseDTO);
        }

        public async Task<UserModelResponseDTO?> GetUserProfile()
        {
            return await GetSecureItemAsync<UserModelResponseDTO>(USERPROFILE);
        }

        public async Task SetRememberMe(AuthenticationRequestDTO authenticationRequestDTO)
        {

            await SetSecureItem(R3M3M4M3, authenticationRequestDTO);
        }

        public async Task<AuthenticationRequestDTO?> GetRememberMe()
        {
            return await GetSecureItemAsync<AuthenticationRequestDTO>(R3M3M4M3);
        }







        public async Task SetSecureItem<T>(string key, T value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            var protectedData = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(jsonData));
            await _localStorageService.SetItemAsync(key, protectedData);
        }

        public async Task<T?> GetSecureItemAsync<T>(string key)
        {
            var protectedData = await _localStorageService.GetItemAsync<string>(key);
            if (protectedData == null)
            {
                return default(T);
            }

            try
            {
                var jsonData = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(protectedData));
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e}");
                return default(T);
            }
        }

      
    }
}
