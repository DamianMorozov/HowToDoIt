// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIDemo.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MediaItemPage : Page
{
    public MediaItemViewModel ViewModel { get; }

    public MediaItemPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<MediaItemViewModel>();
    }
}
