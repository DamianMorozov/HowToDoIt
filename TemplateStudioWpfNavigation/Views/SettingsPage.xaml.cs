using System.Windows.Controls;

using TemplateStudioWpfNavigation.ViewModels;

namespace TemplateStudioWpfNavigation.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
