using GameScorecardsModels;
using GameScorecardsModels.Games;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services.Games
{
    public interface IGamesService
    {
        Task<RestResponse<IEnumerable<Game>, ErrorResponse>> GetAllMyGamesAsync();
    }

    public class GamesService : RestService, IGamesService
    {
        public GamesService(HttpClient client) : base(client)
        {
        }

        public async Task<RestResponse<IEnumerable<Game>, ErrorResponse>> GetAllMyGamesAsync()
        {
            return await GetAsync<IEnumerable<Game>>("api/v1/games/allmygames");
        }
    }
}
