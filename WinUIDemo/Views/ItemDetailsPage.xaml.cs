
namespace WinUIDemo.Views;

public sealed partial class ItemDetailsPage : Page
{
    public ItemDetailsViewModel ViewModel { get; }

    public ItemDetailsPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<ItemDetailsViewModel>();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        var selectedItemId = (int)e.Parameter;
        if (selectedItemId > 0)
            ViewModel.InitializeItemDetailData(selectedItemId);
    }
}