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
    }
}
