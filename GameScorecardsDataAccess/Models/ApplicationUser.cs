using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameScorecardsDataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
