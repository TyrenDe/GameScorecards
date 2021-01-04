using System;
using System.ComponentModel.DataAnnotations;

namespace GameScorecardsModels.Account
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
