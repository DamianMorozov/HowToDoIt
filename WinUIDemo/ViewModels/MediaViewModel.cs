namespace WinUIDemo.ViewModels;

public sealed class MediaViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    public ICommand AddEditCommand { get; }
    public ICommand DeleteCommand { get; }
    private bool CanDeleteItem() => SelectedMediaItem is not null;

    private string _selectedMedium = default!;
    public string SelectedMedium { get => _selectedMedium; set => SetProperty(ref _selectedMedium, value); }
    private ObservableCollection<MediaItem> _items = default!;
    public ObservableCollection<MediaItem> Items { get => _items; set => SetProperty(ref _items, value); }
    private bool _isLoaded;
    public bool IsLoaded { get => _isLoaded; set => SetProperty(ref _isLoaded, value); }
    private ObservableCollection<MediaItem> _allItems = default!;
    public ObservableCollection<MediaItem> AllItems { get => _allItems; set => SetProperty(ref _allItems, value); }
    private ObservableCollection<string> _mediums = default!;
    public ObservableCollection<string> Mediums { get => _mediums; set => SetProperty(ref _mediums, value); }
    private ObservableCollection<string> _allMmediums = default!;
    public ObservableCollection<string> AllMediums { get => _allMmediums; set => SetProperty(ref _allMmediums, value); }
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
    private int _additionalItemCount;
    public int AdditionalItemCount { get => _additionalItemCount; set => SetProperty(ref _additionalItemCount, value); }

    public MediaViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
        AddEditCommand = new RelayCommand(AddEditItem);
        DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
        PopulateData();
        SelectedMedium = Mediums.FirstOrDefault() ?? string.Empty;
        AdditionalItemCount = 1;
    }

    private void PopulateData()
    {
        if (_isLoaded) return;
        IsLoaded = true;
        foreach (var item in _dataService.GetItems())
        {
            Items.Add(item);
        }
        AllItems = new ObservableCollection<MediaItem>(Items);
        Mediums = new ObservableCollection<string>(AllMediums);
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
        _navigationService.NavigateTo(nameof(ItemDetailsPage), selectedItemId);
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
            from item in _allItems
            where string.IsNullOrWhiteSpace(SelectedMedium) || SelectedMedium == nameof(EnumItemType.All) || SelectedMedium == item.MediaType.ToString()
            select item).ToList();
        Items.Clear();
        Items = new ObservableCollection<MediaItem>(updatedItems);
    }
}
