using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MudBlazorUI.Auth.DTOs;
using Microsoft.IdentityModel.Tokens;
using MudBlazorUI.Core.DTOs.Common;
using System.Text;
using System.Runtime.CompilerServices;

namespace MudBlazorUI.Auth.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJwtAuthenticationService jwtAuthenticationService;
        private readonly AuthenticationState anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthenticationStateProvider(IJwtAuthenticationService jwtAuthenticationService)
        {
            this.jwtAuthenticationService = jwtAuthenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await jwtAuthenticationService.GetJwtAsync();

                if (string.IsNullOrEmpty(token))
                {
                    return await Task.FromResult(anonymous);
                }

                var (name, id, role) = await GetClaims(token);

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
                {
                    return await Task.FromResult(anonymous);
                }

                var claimsPrincipal = SetClaimsPrincipal(name, id, role);
                if (claimsPrincipal == null)
                {
                    return await Task.FromResult(anonymous);
                }

                return new AuthenticationState(claimsPrincipal);
            }
            catch (Exception ex) { 
                return await Task.FromResult(anonymous);    
            
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            await jwtAuthenticationService.LogoutAcync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            foreach (var claim in token.Claims)
            {
                claims.Add(claim);
            }

            return claims;
        }

        private async Task<(string?, string?, string?)>  GetClaims(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
            {
                return (null, null, null);
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            Console.WriteLine("Token is " + token);
            var (principals,isRefreshed) = await ValidateTokenAsync(jwt);
            if (isRefreshed) {


                jwt = await jwtAuthenticationService.GetJwtAsync();
                (principals, isRefreshed) = await ValidateTokenAsync(jwt);




            }

            /*foreach (var claim in token.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }*/

            // Use custom claim types based on your JWT structure
            var name = principals.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value; // Custom claim type
            var id = principals.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; // Custom claim type
            var role = principals.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; // Custom claim type
            Console.WriteLine(name + id + role);

            return (name, id, role);
        }

        public static ClaimsPrincipal SetClaimsPrincipal(string name, string id, string role)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return new ClaimsPrincipal();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, id)
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        }

        public async Task UpdateAuthenticationState()
        {
           
            var token = await jwtAuthenticationService.GetJwtAsync();
            

            ClaimsPrincipal claimsPrincipal;

            if (string.IsNullOrEmpty(token))
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            }
            else
            {
                var (name, id, role) =await GetClaims(token);
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
                {
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                }
                else
                {
                    claimsPrincipal = SetClaimsPrincipal(name, id, role);
                }
            }
          
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            
        }

        public async Task<(ClaimsPrincipal?,bool)> ValidateTokenAsync(string token)
        {
            var ifRefreshed = false;

            
            try
            {
                // This key should be stored securely, not hardcoded.
                string key = "QuoKCRHInuiuL7unO9J8grY5nFxeTFISrUpue5nBSGPBVHYbiYwRaKci0LOVuvDL";

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, 
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // Ensure the token is a JWT
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    // validate the signing algorithm
                    var signingAlgorithm = jwtSecurityToken.Header.Alg;
                    if (signingAlgorithm != SecurityAlgorithms.HmacSha256)
                    {
                        throw new SecurityTokenException("Invalid signing algorithm");
                    }

                    // Check token's expiration and not before claims
                    var exp = jwtSecurityToken.ValidTo;
                    var nbf = jwtSecurityToken.ValidFrom;
                    var now = DateTime.UtcNow;

                    if (now < nbf || now > exp)
                    {


                            throw new SecurityTokenException("Token is not yet valid or has expired");

                    

                    }

                    Console.WriteLine($"Token valid from: {nbf}, valid to: {exp}");
                }

                return (principal, ifRefreshed);
            }
            catch (Exception ex)
            {

                var refreshSucceeded = await jwtAuthenticationService.Refresh();
             ;


                if (refreshSucceeded)
                {

                    Console.Write("refresh requested form Validate token async");
                    await UpdateAuthenticationState();
                    return (null, true);


                }
                else {

                    Console.WriteLine($"Token validation failed: {ex.Message}");
                    await jwtAuthenticationService.LogoutAcync();
                    return (null, ifRefreshed);

                }
               
            }
        }
    }
}
