using AutoMapper;

namespace GameScorecardsAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameScorecardsDataAccess.Models.Game, GameScorecardsModels.Games.Game>().ReverseMap();
        }
    }
}
