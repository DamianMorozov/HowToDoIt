
namespace WinUIDemo.ViewModels;

public sealed class ItemDetailsViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private ObservableCollection<string> _locationTypes = [];
    public ObservableCollection<string> LocationTypes { get => _locationTypes; set => SetProperty(ref _locationTypes, value); }
    private ObservableCollection<string> _mediums = default!;
    public ObservableCollection<string> Mediums { get => _mediums; set => SetProperty(ref _mediums, value); }
    private ObservableCollection<string> _itemTypes = default!;
    public ObservableCollection<string> ItemTypes { get => _itemTypes; set => SetProperty(ref _itemTypes, value); }
    public int _itemId;
    public int _selectedItemId = -1;
    private bool _isDirty;
    public bool IsDirty { get => _isDirty; set => SetProperty(ref _isDirty, value); }
    private string _itemName = default!;
    public string ItemName
    {
        get => _itemName; set
        {
            if (_itemName == value) return;
            SetProperty(ref _itemName, value);
            IsDirty = true;
        }
    }
    private string _selectedMedium = default!;
    public string SelectedMedium
    {
        get => _selectedMedium; set
        {
            if (_selectedMedium == value) return;
            SetProperty(ref _selectedMedium, value);
            IsDirty = true;
        }
    }
    private string _selectedItemType = default!;
    public string SelectedItemType
    {
        get => _selectedItemType; set
        {
            if (_selectedItemType == value) return;
            SetProperty(ref _selectedItemType, value);
            IsDirty = true;
            Mediums.Clear();
            if (!string.IsNullOrWhiteSpace(value))
            {
                foreach (var med in _dataService.GetMediums((EnumItemType)Enum.Parse(typeof(EnumItemType), SelectedItemType)).Select(m => m.Name))
                    Mediums.Add(med);
            }
        }
    }
    private string _selectedLocation = default!;
    public string SelectedLocation
    {
        get => _selectedLocation;
        set
        {
            if (_selectedLocation == value) return;
            SetProperty(ref _selectedLocation, value);
            IsDirty = true;
        }
    }


    public ItemDetailsViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
        _itemTypes = [];

        SaveCommand = new RelayCommand(SaveItem, () => IsDirty);
        CancelCommand = new RelayCommand(Cancel);
        PopulateLists();
    }

    public void InitializeItemDetailData(int selectedItemId)
    {
        _selectedItemId = selectedItemId;
        InitializeItemData();
    }

    private void InitializeItemData()
    {
        PopulateLists();
        PopulateExistingItem();
        IsDirty = false;
    }

    private void PopulateExistingItem()
    {
        if (_selectedItemId > 0)
        {
            var item = _dataService.GetItem(_selectedItemId);
            Mediums.Clear();
            foreach (var medium in _dataService.GetMediums(item.MediaType).Select(m => m.Name))
                Mediums.Add(medium);
            _itemId = item.Id;
            ItemName = item.Name;
            SelectedMedium = item.MediumInfo.Name;
            SelectedLocation = item.Location.ToString();
            SelectedItemType = item.MediaType.ToString();
        }
    }

    private void PopulateLists()
    {
        ItemTypes.Clear();
        foreach (var iType in Enum.GetNames(typeof(EnumItemType)))
            ItemTypes.Add(iType);
        LocationTypes.Clear();
        foreach (var lType in Enum.GetNames(typeof(EnumLocationType)))
            LocationTypes.Add(lType);
        Mediums = [];
    }

    private void SaveItem()
    {
        MediaItem item;
        if (_itemId > 0)
        {
            item = _dataService.GetItem(_itemId);
            item.Name = ItemName;
            item.Location = (EnumLocationType)Enum.Parse(typeof(EnumLocationType), SelectedLocation);
            item.MediaType = (EnumItemType)Enum.Parse(typeof(EnumItemType), SelectedItemType);
            item.MediumInfo = _dataService.GetMedium(SelectedMedium);
            _dataService.UpdateItem(item);
        }
        else
        {
            item = new MediaItem
            {
                Name = ItemName,
                Location = (EnumLocationType)Enum.Parse(typeof(EnumLocationType), SelectedLocation),
                MediaType = (EnumItemType)Enum.Parse(typeof(EnumItemType), SelectedItemType),
                MediumInfo = _dataService.GetMedium(SelectedMedium)
            };
            _dataService.AddItem(item);
        }
        _navigationService.GoBack();
    }

    private void Cancel()
    {
        _navigationService.GoBack();
    }
}
