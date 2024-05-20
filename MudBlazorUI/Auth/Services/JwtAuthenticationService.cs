using MudBlazorUI.Auth.DTOs;
using Blazored.SessionStorage;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using MudBlazor;
using MudBlazorUI.Core.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;
using MudBlazorUI.Core.DTOs.Common;


namespace MudBlazorUI.Auth.DTOs
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ISessionStorageService _sessionStorageService;
     

        private string? _jwtCache;
        private const string JWT_KEY = nameof(JWT_KEY);
        private const string RERESH_KEY = nameof(RERESH_KEY);

        public JwtAuthenticationService(IHttpClientFactory factory, ISessionStorageService sessionStorageService)
        {
            _factory = factory;
            _sessionStorageService = sessionStorageService;
            

        }

        //Get Jwt Token
         public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);

            return await _sessionStorageService.GetItemAsync<string>(JWT_KEY); 
        }


        // Logout User
        public async Task LogoutAcync()
        {
            await _sessionStorageService.RemoveItemAsync(JWT_KEY);
            await _sessionStorageService.RemoveItemAsync(RERESH_KEY);

            _jwtCache = null;
            await Console.Out.WriteAsync("logout");
        }

        //Login User
        public async Task<AuthenticationResponseDTO?> LoginAsync(AuthenticationRequestDTO request)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("api/Account/Login", request);
            var content = await result.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(content);
            if (result.IsSuccessStatusCode)
            {
                
                if (response!.JwtToken != null)
                {
                    //store the token in session storage
                    await _sessionStorageService.SetItemAsync(JWT_KEY, response.JwtToken);
                    await _sessionStorageService.SetItemAsync(RERESH_KEY, response.RefreshToken);
                }

           

                return response;
            }

            return response;

        }

        public async Task<UserModelResponseDTO?> GetUserDetails()
        {
            
            var result = await _factory.CreateClient("ServerApi").GetAsync("api/Account/Get-User-Details");
           
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModelResponseDTO>(content);
            }

            return null;

        }


        public async Task<bool> Refresh()
        {
            var request = new TokenInfoDTO
            {
                JwtToken = await _sessionStorageService.GetItemAsync<string>(JWT_KEY),
                RefreshToken = await _sessionStorageService.GetItemAsync<string>(RERESH_KEY)
            };


            if (!string.IsNullOrEmpty(request.RefreshToken)) {

                var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("api/Account/Request-RefreshToken", request);
                Console.WriteLine("refresh " + result.StatusCode);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(content);

                    await _sessionStorageService.SetItemAsync(JWT_KEY, response!.JwtToken);
                    await _sessionStorageService.SetItemAsync(RERESH_KEY, response.RefreshToken);

                    _jwtCache = response.JwtToken;

                    Console.WriteLine("Refreshed");

                    return true;

                }
            }


            await LogoutAcync();
            Console.WriteLine("refresh fail");

            return false;
        }
    }
}
