using GameScorecardsClient.Services.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services.Authentication
{
    public class GameScorecardAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient m_HttpClient;
        private readonly IStorageService m_Storage;

        public GameScorecardAuthenticationStateProvider(HttpClient client, IStorageService storage)
        {
            m_HttpClient = client;
            m_Storage = storage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await m_Storage.GetAuthTokenAsync();
            if (token == null)
            {
                m_HttpClient.DefaultRequestHeaders.Authorization = null;
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwtAuthType")));
        }

        public void NotifyUserLoggedIn(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var authenticatedUser = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwtAuthType")));
            var authState = Task.FromResult(authenticatedUser);
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
