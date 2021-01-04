using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace GameScorecardsClient.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager m_NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        private bool m_NotAuthorized = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;
            if (authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = m_NavigationManager.ToBaseRelativePath(m_NavigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    m_NavigationManager.NavigateTo("login", true);
                }
                else
                {
                    m_NavigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                m_NotAuthorized = true;
            }
        }
    }
}
