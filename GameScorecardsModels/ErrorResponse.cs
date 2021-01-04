using System;
using System.Collections.Generic;
using System.Text;

namespace GameScorecardsModels
{
    public class ErrorResponse
    {
        public IEnumerable<string> Messages { get; set; }
    }
}
