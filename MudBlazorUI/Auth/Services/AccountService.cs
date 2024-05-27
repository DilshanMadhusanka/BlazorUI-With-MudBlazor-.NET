using MudBlazorUI.Core.DTOs.Response;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MudBlazorUI.Auth.Services
{
    public class AccountService : IAccountService
    {

        private readonly IHttpClientFactory _factory;

        public AccountService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("api/Account/ForgotPassword-Verification-Sender", email);
           
            if (result.IsSuccessStatusCode) {
                return true;

            }
            else
            {
                return false ;
            }
        }

        public async Task<UserModelResponseDTO?> GetUserDetaiils() {

            var result = await _factory.CreateClient("ServerApi").GetAsync("api/Account/Get-User-Details");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<UserModelResponseDTO>(content);
                return response;

            }

            else return null;
        }


        public async Task<string?> Resend2FACode(string email)
        {
            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("api/Account/Resend-2FAVerificationCode",email);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(content);
                return response!.Message;
                
            }

            else return null;
        }

    }
}
