using GameScorecardsClient.Services;
using GameScorecardsClient.Services.Authentication;
using GameScorecardsModels;
using GameScorecardsModels.Account;
using Microsoft.AspNetCore.Components;
using MvvmBlazor.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;

namespace GameScorecardsClient.ViewModels.Authentication
{
    public class LoginViewModel : ViewModelBase
    {
        protected readonly IAuthenticationService m_AuthenticationService;
        protected readonly NavigationManager m_NavigationManager;

        private string m_Email;
        private string m_Password;
        private bool m_IsBusy = false;
        private string m_Message = string.Empty;
        private string m_ReturnUrl;

        [Required]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get => m_Email; set => Set(ref m_Email, value); }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get => m_Password; set => Set(ref m_Password, value); }

        public bool IsBusy { get => m_IsBusy; set => Set(ref m_IsBusy, value); }

        public string Message { get => m_Message; set => Set(ref m_Message, value); }

        public string ReturnUrl { get => m_ReturnUrl; set => Set(ref m_ReturnUrl, value); }

        public LoginViewModel(IAuthenticationService authenticationService, NavigationManager navigationManager)
        {
            m_AuthenticationService = authenticationService;
            m_NavigationManager = navigationManager;
        }

        public async Task SignInAsync()
        {
            IsBusy = true;

            var request = new RestRequest<SignInRequest>
            {
                Request = new SignInRequest { Email = Email, Password = Password },
            };

            var response = await m_AuthenticationService.SignInAsync(request);
            HandleSignInResponse(response);

            IsBusy = false;
        }

        protected void HandleSignInResponse(RestResponse<SignInResponse> response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Message = response.Message;
            }
            else if (response.Result?.Succeeded ?? false)
            {
                var absoluteUri = new Uri(m_NavigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    m_NavigationManager.NavigateTo("/");
                }
                else
                {
                    m_NavigationManager.NavigateTo($"/{ReturnUrl}");
                }
            }
        }
    }
}
