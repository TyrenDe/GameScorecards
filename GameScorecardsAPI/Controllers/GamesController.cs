using AutoMapper;
using GameScorecardsDataAccess.Repositories;
using GameScorecardsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameScorecardsAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGamesRepository m_GamesRepository;
        private readonly IMapper m_Mapper;

        public GamesController(IGamesRepository gamesRespository, IMapper mapper)
        {
            m_GamesRepository = gamesRespository;
            m_Mapper = mapper;
        }

        [HttpGet("allmygames")]
        public async Task<RestResponse<IEnumerable<GameScorecardsModels.Games.Game>>> GetAllMyGamesAsync([FromQuery]string requestId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var games = await m_GamesRepository.GetAllGamesByUserIdAsync(userId);
            var mappedGames = m_Mapper.Map<
                IEnumerable<GameScorecardsDataAccess.Models.Game>,
                IEnumerable<GameScorecardsModels.Games.Game>>(games);

            var response = new RestResponse<IEnumerable<GameScorecardsModels.Games.Game>>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Result = mappedGames,
            };

            return response;
        }

        [HttpPost("create")]
        public async Task<RestResponse<GameScorecardsModels.Games.Game>> CreateGameAsync([FromBody]RestRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            await Task.Delay(100);
            throw new NotImplementedException(userId);
            //var games = await m_GamesRepository.GetAllGamesByUserIdAsync(userId);
            //var mappedGames = m_Mapper.Map<
            //    IEnumerable<GameScorecardsDataAccess.Models.Game>,
            //    IEnumerable<GameScorecardsModels.Games.Game>>(games);
            //return Ok(mappedGames);
        }

        [HttpPatch("addplayertogame")]
        public async Task<RestResponse<GameScorecardsModels.Games.Game>> AddPlayerToGameAsync([FromBody] RestRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            await Task.Delay(100);
            throw new NotImplementedException(userId);
            //var games = await m_GamesRepository.GetAllGamesByUserIdAsync(userId);
            //var mappedGames = m_Mapper.Map<
            //    IEnumerable<GameScorecardsDataAccess.Models.Game>,
            //    IEnumerable<GameScorecardsModels.Games.Game>>(games);
            //return Ok(mappedGames);
        }
    }
}
