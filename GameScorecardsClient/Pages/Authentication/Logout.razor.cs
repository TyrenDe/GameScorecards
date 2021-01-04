using GameScorecardsClient.Services.Authentication;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameScorecardsClient.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        private IAuthenticationService m_AuthenticationService { get; set; }

        [Inject]
        private NavigationManager m_NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await m_AuthenticationService.LogoutAsync();
            m_NavigationManager.NavigateTo("/");
        }
    }
}
