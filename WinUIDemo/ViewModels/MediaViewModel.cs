namespace WinUIDemo.ViewModels;

public sealed partial class MediaViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    [ObservableProperty]
    public partial string SelectedMedium { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<MediaItem> Items { get; set; }
    [ObservableProperty]
    public partial bool IsLoaded { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<MediaItem> AllItems { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<string> Mediums { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<string> AllMediums { get; set; }
    [ObservableProperty]
    public partial int AdditionalItemCount { get; set; }
    private MediaItem? _selectedMediaItem;
    public MediaItem? SelectedMediaItem
    {
        get => _selectedMediaItem;
        set {
            if (SetProperty(ref _selectedMediaItem, value))
            {
                ((RelayCommand)DeleteCommand).NotifyCanExecuteChanged();
            }
        }
    }

    public ICommand AddEditCommand { get; }
    public ICommand DeleteCommand { get; }

    public MediaViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
        Items = [];
        AllItems = [];
        Mediums = [];
        AllMediums = [];

        PopulateData();
        SelectedMedium = Mediums.FirstOrDefault() ?? string.Empty;
        AdditionalItemCount = 1;

        AddEditCommand = new RelayCommand(AddEditItem);
        DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
    }

    private bool CanDeleteItem() => SelectedMediaItem is not null;

    private void PopulateData()
    {
        if (IsLoaded) return;
        IsLoaded = true;
        Items.Clear();
        foreach (var item in _dataService.GetItems())
        {
            Items.Add(item);
        }
        AllItems = [.. Items];
        Mediums = [.. AllMediums];
        foreach (var itemType in _dataService.GetItemTypes())
        {
            Mediums.Add(itemType.ToString());
        }
        SelectedMedium = Mediums[0];
    }

    public void AddEditItem()
    {
        var selectedItemId = -1;
        if (SelectedMediaItem is not null)
            selectedItemId = SelectedMediaItem.Id;
        _navigationService.NavigateTo(typeof(ItemDetailsViewModel).FullName!, selectedItemId);
    }

    public void DeleteItem()
    {
        if (SelectedMediaItem is not null)
        {
            AllItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
        }
    }

    public void ListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        AddEditItem();
    }

    public void FilterChanged(object sender, SelectionChangedEventArgs e)
    {
        var updatedItems = (
            from item in AllItems
            where string.IsNullOrWhiteSpace(SelectedMedium) || SelectedMedium == nameof(EnumItemType.All) || SelectedMedium == item.MediaType.ToString()
            select item).ToList();
        Items.Clear();
        Items = [.. updatedItems];
    }
}
