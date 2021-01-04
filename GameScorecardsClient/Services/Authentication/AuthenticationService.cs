using GameScorecardsClient.Services.Storage;
using GameScorecardsModels;
using GameScorecardsModels.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<RestResponse<SignInResponse, ErrorResponse>> RegisterAsync(RegisterRequest request);
        Task<RestResponse<SignInResponse, ErrorResponse>> SignInAsync(SignInRequest request);
        Task LogoutAsync();
    }

    public class AuthenticationService : RestService, IAuthenticationService
    {
        private readonly IStorageService m_Storage;
        private readonly AuthenticationStateProvider m_AuthenticationStateProvider;

        public AuthenticationService(HttpClient client, IStorageService storage, AuthenticationStateProvider authenticationStateProvider) : base(client)
        {
            m_Storage = storage;
            m_AuthenticationStateProvider = authenticationStateProvider;
            ((GameScorecardAuthenticationStateProvider)m_AuthenticationStateProvider).NotifyUserLogout();
        }

        public async Task LogoutAsync()
        {
            await m_Storage.ClearAuthTokenAsync();
            ((GameScorecardAuthenticationStateProvider)m_AuthenticationStateProvider).NotifyUserLogout();
        }

        public async Task<RestResponse<SignInResponse, ErrorResponse>> RegisterAsync(RegisterRequest request)
        {
            var response = await PostAsync<RegisterRequest, SignInResponse>("api/v1/account/register", request);
            return await HandleResponseAsync(response);
        }

        public async Task<RestResponse<SignInResponse, ErrorResponse>> SignInAsync(SignInRequest request)
        {
            var response = await PostAsync<SignInRequest, SignInResponse>("api/v1/account/signin", request);
            return await HandleResponseAsync(response);
        }

        private async Task<RestResponse<SignInResponse, ErrorResponse>> HandleResponseAsync(RestResponse<SignInResponse, ErrorResponse> response)
        {
            if (response?.Response?.Succeeded ?? false)
            {
                await m_Storage.SetAuthTokenAsync(response.Response.Token);
                ((GameScorecardAuthenticationStateProvider)m_AuthenticationStateProvider).NotifyUserLoggedIn(response.Response.Token);
            }
            else
            {
                await LogoutAsync();
            }

            return response;
        }
    }
}
