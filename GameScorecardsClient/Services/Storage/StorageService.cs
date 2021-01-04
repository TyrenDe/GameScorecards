using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services.Storage
{
    public interface IStorageService
    {
        Task<string> GetAuthTokenAsync();
        Task SetAuthTokenAsync(string token);
        Task ClearAuthTokenAsync();
    }

    public class StorageService : IStorageService
    {
        private readonly ILocalStorageService m_LocalStorageService;

        public StorageService(ILocalStorageService localStorageService)
        {
            m_LocalStorageService = localStorageService;
        }

        public async Task<string> GetAuthTokenAsync()
        {
            return await m_LocalStorageService.GetItemAsync<string>("AuthToken");
        }

        public async Task SetAuthTokenAsync(string token)
        {
            await m_LocalStorageService.SetItemAsync<string>("AuthToken", token);
        }

        public async Task ClearAuthTokenAsync()
        {
            await m_LocalStorageService.RemoveItemAsync("AuthToken");
        }
    }
}
