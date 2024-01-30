using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace HospitalAssessment.Client.Utilities
{
    public class HospitalAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;

        public HospitalAuthStateProvider(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenFromLocalStorage();
            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(GetClaimsPrincipalFromToken(token));
        }

        public void SetUserAsAuthenticated(string userName)
        {
            var principle = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }, "clientAuth"));
            var authState = Task.FromResult(new AuthenticationState(principle));
            NotifyAuthenticationStateChanged(authState);
        }

        public void SetUserAsLoggedOut()
        {
            var principle = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(principle));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<string> GetTokenFromLocalStorage()
        {
            var userData = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", "usr").ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(userData))
            {
                var dataArray = userData.Split(';', 2);
                if (dataArray.Length == 2)
                    return dataArray[1];
            }
            return string.Empty;
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null || jwtToken.Payload == null) return new ClaimsPrincipal(new ClaimsIdentity());

            var claims = new ClaimsIdentity("jwt");
            claims.AddClaim(new Claim(ClaimTypes.Authentication, jwtToken.RawData));
            claims.AddClaims(jwtToken.Claims);

            return new ClaimsPrincipal(claims);
        }
    }
}
