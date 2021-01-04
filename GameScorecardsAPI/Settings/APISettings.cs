using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameScorecardsAPI.Settings
{
    public class APISettings
    {
        public string SecretKey { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
