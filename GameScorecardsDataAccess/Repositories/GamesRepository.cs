using GameScorecardsDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameScorecardsDataAccess.Repositories
{
    public interface IGamesRepository
    {
        Task<IEnumerable<Game>> GetAllGamesByUserIdAsync(string userId);
    }
    public class GamesRepository : IGamesRepository
    {
        private readonly ApplicationDbContext m_DB;

        public GamesRepository(ApplicationDbContext db)
        {
            m_DB = db;
        }

        public async Task<IEnumerable<Game>> GetAllGamesByUserIdAsync(string userId)
        {
            return await m_DB.Games
                .Where(g => g.Players.Any(p => p.Id == userId))
                .ToListAsync();
        }
    }
}
