using System.Windows.Controls;

using TemplateStudioWpfNavigation.ViewModels;

namespace TemplateStudioWpfNavigation.Views
{
    public partial class DataGridPage : Page
    {
        public DataGridPage(DataGridViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
