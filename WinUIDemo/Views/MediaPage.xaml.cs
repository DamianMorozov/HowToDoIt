namespace WinUIDemo.Views;

public sealed partial class MediaPage : Page
{
    public MediaViewModel ViewModel { get; }

    public MediaPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<MediaViewModel>();
    }
}
