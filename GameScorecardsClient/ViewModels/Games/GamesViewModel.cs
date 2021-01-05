using GameScorecardsClient.Services.Games;
using GameScorecardsModels.Games;
using MvvmBlazor.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameScorecardsClient.ViewModels.Games
{
    public class GamesViewModel : ViewModelBase
    {
        private readonly IGamesService m_GamesService;

        private IEnumerable<Game> m_Games = Array.Empty<Game>();
        public IEnumerable<Game> Games
        {
            get => m_Games;
            private set => Set(ref m_Games, value);
        }

        public override async Task OnInitializedAsync()
        {
            var response = await m_GamesService.GetAllMyGamesAsync();
            if (response?.Result!= null)
            {
                Games = response.Result;
            }

            await base.OnInitializedAsync();
        }

        public GamesViewModel(IGamesService gamesService)
        {
            m_GamesService = gamesService;
        }
    }
}
