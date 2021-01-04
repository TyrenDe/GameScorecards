using System.Collections.Generic;

namespace GameScorecardsDataAccess.Models
{
    public class Game
    {
        public int Id { get; set; }

        public GameTypes GameType { get; set; }

        public virtual ICollection<ApplicationUser> Players { get; set; }
    }
}
