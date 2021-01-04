using MvvmBlazor.ViewModel;

namespace GameScorecardsClient.ViewModels.Shared
{
    public class NavBarViewModel : ViewModelBase
    {
        private bool m_CollapseNavMenu = true;
        private string m_NavMenuCssClass = "collapse";

        public string NavMenuCssClass { get => m_NavMenuCssClass; set => Set(ref m_NavMenuCssClass, value); }

        public void ToggleNavMenu()
        {
            m_CollapseNavMenu = !m_CollapseNavMenu;
            NavMenuCssClass = m_CollapseNavMenu ? "collapse" : null;
        }
    }
}
