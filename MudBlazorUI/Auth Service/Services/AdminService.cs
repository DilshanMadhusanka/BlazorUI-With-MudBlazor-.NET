﻿using MudBlazorUI.Auth_Service.DTOs.Request;
using MudBlazorUI.Core.DTOs.Response;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MudBlazorUI.Auth_Service.Services
{
    public class AdminService : IAdminService
    {

        private readonly IHttpClientFactory _factory;

        public AdminService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<UserModelResponseDTO>?> GetAllUsersDetaiils(string searchString)
        {

            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("ApiGateWay/Auth-api/Admin/getUsers-details", searchString);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<IEnumerable<UserModelResponseDTO>>(content);
                return response;

            }

            else return null;
        } 
        public async Task<bool> Update(UpdateUserRequest updateUserRequest)
        {

            var result = await _factory.CreateClient("ServerApi").PostAsJsonAsync("ApiGateWay/Auth-api/Account/Update", updateUserRequest);
            if (result.IsSuccessStatusCode)
            {
               return true;

            }

            else return false;
        }
    }
}