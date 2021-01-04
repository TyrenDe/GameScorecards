using System.Collections.Generic;

namespace GameScorecardsModels.Games
{
    public class Game
    {
        public int Id { get; set; }
        public GameTypes GameType { get; set; }

        public virtual IEnumerable<User> Players { get; set; }
    }
}
