using System.Windows.Controls;

using TemplateStudioWpfNavigation.ViewModels;

namespace TemplateStudioWpfNavigation.Views
{
    public partial class ContentGridDetailPage : Page
    {
        public ContentGridDetailPage(ContentGridDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
