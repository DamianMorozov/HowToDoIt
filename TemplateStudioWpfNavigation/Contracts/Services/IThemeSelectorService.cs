using TemplateStudioWpfNavigation.Models;

namespace TemplateStudioWpfNavigation.Contracts.Services
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void SetTheme(AppTheme theme);

        AppTheme GetCurrentTheme();
    }
}
