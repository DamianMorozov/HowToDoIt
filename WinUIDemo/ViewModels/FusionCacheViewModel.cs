namespace WinUIDemo.ViewModels;

public sealed partial class FusionCacheViewModel : ObservableRecipient
{
    [ObservableProperty]
    public partial bool IsLoaded { get; set; }
    public ICommand ResetCommand { get; }

    public FusionCacheViewModel()
    {
        ResetCommand = new AsyncRelayCommand(ResetAsync);

        PopulateData();
    }

    private void PopulateData()
    {
        if (IsLoaded) return;
        IsLoaded = true;
    }

    public async Task ResetAsync()
    {
        await Task.CompletedTask;
    }
}
