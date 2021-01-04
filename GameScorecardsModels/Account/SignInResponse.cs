using System;

namespace GameScorecardsModels.Account
{
    public class SignInResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
    }
}
