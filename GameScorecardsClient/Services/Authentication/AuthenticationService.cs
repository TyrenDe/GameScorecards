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
        Task<RestResponse<SignInResponse>> RegisterAsync(RestRequest<RegisterRequest> request);
        Task<RestResponse<SignInResponse>> SignInAsync(RestRequest<SignInRequest> request);
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

        public async Task<RestResponse<SignInResponse>> RegisterAsync(RestRequest<RegisterRequest> request)
        {
            var response = await PostAsync<RegisterRequest, SignInResponse>("api/v1/account/register", request);
            return await HandleResponseAsync(response);
        }

        public async Task<RestResponse<SignInResponse>> SignInAsync(RestRequest<SignInRequest> request)
        {
            var response = await PostAsync<SignInRequest, SignInResponse>("api/v1/account/signin", request);
            return await HandleResponseAsync(response);
        }

        private async Task<RestResponse<SignInResponse>> HandleResponseAsync(RestResponse<SignInResponse> response)
        {
            if (response?.Result?.Succeeded ?? false)
            {
                await m_Storage.SetAuthTokenAsync(response.Result.Token);
                ((GameScorecardAuthenticationStateProvider)m_AuthenticationStateProvider).NotifyUserLoggedIn(response.Result.Token);
            }
            else
            {
                await LogoutAsync();
            }

            return response;
        }
    }
}
