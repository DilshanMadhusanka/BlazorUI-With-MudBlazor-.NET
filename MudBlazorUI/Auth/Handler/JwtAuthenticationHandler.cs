using MudBlazorUI.Auth.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

namespace MudBlazorUI.Auth.Handler
{
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly ILogger<JwtAuthenticationHandler> _logger;
        private readonly NavigationManager _navigationManager;
        private readonly SemaphoreSlim _refreshLock = new SemaphoreSlim(1, 1);

        public JwtAuthenticationHandler(IJwtAuthenticationService jwtAuthenticationService,
            ILogger<JwtAuthenticationHandler> logger, NavigationManager navigationManager)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            _logger = logger;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retrieve JWT token
            var jwt = await _jwtAuthenticationService.GetJwtAsync();
            _logger.LogInformation("JWT token retrieved: {jwt}", jwt);

            // Set Authorization header if JWT token is not empty or null
            if (!string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
            }

            // Call the base SendAsync method to continue processing the request pipeline
            var response = await base.SendAsync(request, cancellationToken);

            if (!string.IsNullOrEmpty(jwt) && response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _refreshLock.WaitAsync(cancellationToken);
                try
                {
                    // Attempt to refresh the token
                    var refreshSucceeded = await _jwtAuthenticationService.Refresh();
                    if (refreshSucceeded)
                    {
                        jwt = await _jwtAuthenticationService.GetJwtAsync();
                        _logger.LogInformation("JWT token refreshed: {jwt}", jwt);

                        // Set Authorization header if JWT token is not empty or null
                        if (!string.IsNullOrEmpty(jwt))
                        {
                            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

                            // Retry the request with the refreshed token
                            response = await base.SendAsync(request, cancellationToken);
                        }
                    }
                    else
                    {
                        // Navigate to login page if refresh fails
                        _navigationManager.NavigateTo("/login");
                    }
                }
                finally
                {
                    _refreshLock.Release();
                }
            }

            return response;
        }
    }
}
