using GameScorecardsClient.Services.Authentication;
using GameScorecardsModels.Account;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GameScorecardsClient.ViewModels.Authentication
{
    public class RegisterViewModel : LoginViewModel
    {
        private string m_Name;
        private string m_ConfirmPassword;

        public RegisterViewModel(IAuthenticationService authenticationService, NavigationManager navigationManager) : base(authenticationService, navigationManager)
        {
        }

        [Required]
        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
        public string ConfirmPassword { get => m_ConfirmPassword; set => Set(ref m_ConfirmPassword, value); }

        public async Task RegisterAsync()
        {
            IsBusy = true;

            var response = await m_AuthenticationService.RegisterAsync(new RegisterRequest { Name = Name, Email = Email, Password = Password });
            HandleResponse(response);

            IsBusy = false;
        }
    }
}
