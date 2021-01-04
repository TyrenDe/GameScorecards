using System;
using System.ComponentModel.DataAnnotations;

namespace GameScorecardsModels.Account
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
