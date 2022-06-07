using System.Windows.Controls;

using TemplateStudioWpfNavigation.ViewModels;

namespace TemplateStudioWpfNavigation.Views
{
    public partial class ContentGridPage : Page
    {
        public ContentGridPage(ContentGridViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
