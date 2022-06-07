using System.Windows.Controls;

namespace TemplateStudioWpfNavigation.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
