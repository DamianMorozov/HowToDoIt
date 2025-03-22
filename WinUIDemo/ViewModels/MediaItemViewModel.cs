namespace WinUIDemo.ViewModels;

public sealed partial class MediaItemViewModel : ObservableObject
{
    private ObservableCollection<string> _locationTypes = new();
    public ObservableCollection<string> LocationTypes { get => _locationTypes; set => SetProperty(ref _locationTypes, value); }
    private ObservableCollection<string> _mediums = new();
    public ObservableCollection<string> Mediums { get => _mediums; set => SetProperty(ref _mediums, value); }
    private ObservableCollection<string> _itemTypes = new();
    public ObservableCollection<string> ItemTypes { get => _itemTypes; set => SetProperty(ref _itemTypes, value); }
    private int _itemId;
    private string _itemName;
    public string ItemName { get => _itemName; set => SetProperty(ref _itemName, value); }
    private string _selectedMedium;
    public string SelectedMedium { get => _selectedMedium; set => SetProperty(ref _selectedMedium, value); }
    private string _selectedItemType;
    public string SelectedItemType { get => _selectedItemType; set => SetProperty(ref _selectedItemType, value); }
    private string _selectedLocation;
    public string SelectedLocation { get => _selectedLocation; set => SetProperty(ref _selectedLocation, value); }

    private bool _isDirty;
    public bool IsDirty { get => _isDirty; set => SetProperty(ref _isDirty, value); }
    private int _selectedItemId = -1;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public MediaItemViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
        _itemName = string.Empty;
        _selectedMedium = string.Empty;
        _selectedItemType = string.Empty;
        _selectedLocation = string.Empty;

        PopulateLists();
        SaveCommand = new RelayCommand(Save, CanSaveItem);
        CancelCommand = new RelayCommand(Cancel);
    }

    public void InitializeItemDetailData(int itemId)
    {
        _selectedItemId = itemId;

        PopulateExistingItem(_dataService);
        IsDirty = false;
    }

    private void PopulateExistingItem(IDataService dataService)
    {
        if (_selectedItemId > 0)
        {
            var item = _dataService.GetItem(_selectedItemId);
            Mediums.Clear();

            foreach (var medium in dataService.GetMediums(item.MediaType).Select(m => m.Name))
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

        Mediums = new();
    }

    //[RelayCommand(CanExecute = nameof(CanSaveItem))]
    private void Save()
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
            item = new()
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

    private bool CanSaveItem() => IsDirty;

    private void OnItemNameChanged(string value) => IsDirty = true;

    private void OnSelectedMediumChanged(string value) => IsDirty = true;

    private void OnSelectedItemTypeChanged(string value)
    {
        IsDirty = true;
        Mediums.Clear();

        if (!string.IsNullOrWhiteSpace(value))
        {
            foreach (var med in _dataService.GetMediums((EnumItemType)Enum.Parse(typeof(EnumItemType), SelectedItemType)).Select(m => m.Name))
                Mediums.Add(med);
        }
    }

    private void OnSelectedLocationChanged(string value)
    {
        IsDirty = true;
    }

    //[RelayCommand]
    private void Cancel()
    {
        _navigationService.GoBack();
    }
}
