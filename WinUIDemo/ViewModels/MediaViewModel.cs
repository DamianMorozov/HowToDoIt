using System.Collections.ObjectModel;

namespace WinUIDemo.ViewModels;

public sealed partial class MediaViewModel : ObservableRecipient
{
    private string _selectedMedium = default!;
    public string SelectedMedium { get => _selectedMedium; set => SetProperty(ref _selectedMedium, value); }
    private ObservableCollection<MediaItem> _items = default!;
    public ObservableCollection<MediaItem> Items { get => _items; set => SetProperty(ref _items, value); }
    private bool _isLoaded = false;
    public bool IsLoaded { get => _isLoaded; set => SetProperty(ref _isLoaded, value); }
    private ObservableCollection<MediaItem> _allItems = default!;
    public ObservableCollection<MediaItem> AllItems { get => _allItems; set => SetProperty(ref _allItems, value); }
    private ObservableCollection<string> _mediums = default!;
    public ObservableCollection<string> Mediums { get => _mediums; set => SetProperty(ref _mediums, value); }
    private MediaItem _selectedMediaItem = default!;
    public MediaItem SelectedMediaItem
    {
        get => _selectedMediaItem;
        set {
            if (SetProperty(ref _selectedMediaItem, value))
            {
                ((RelayCommand)DeleteCommand).NotifyCanExecuteChanged();
            }
        }
    }
    private int _additionalItemCount = default!;
    public int AdditionalItemCount { get => _additionalItemCount; set => SetProperty(ref _additionalItemCount, value); }

    public ICommand AddEditCommand { get; }
    public ICommand DeleteCommand { get; }
    private bool CanDeleteItem() => SelectedMediaItem != null;

    public MediaViewModel()
    {
        AddEditCommand = new RelayCommand(AddEditItem);
        DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
        PopulateData();
        SelectedMedium = Mediums.FirstOrDefault() ?? string.Empty;
        AdditionalItemCount = 1;
    }

    private void PopulateData()
    {
        if (_isLoaded)
            return;
        IsLoaded = true;
        MediaItem cd = new()
        {
            Id = 1,
            Name = "Classical Favorites",
            MediaType = EnumItemType.Music,
            MediumInfo = new()
            {
                Id = 1,
                MediaType = EnumItemType.Music,
                Name = "CD"
            },
        };
        MediaItem book = new()
        {
            Id = 2,
            Name = "Classic Fairy Tails",
            MediaType = EnumItemType.Book,
            MediumInfo = new()
            {
                Id = 2,
                MediaType = EnumItemType.Book,
                Name = "Book"
            },
        };
        MediaItem bluRay = new()
        {
            Id = 3,
            Name = "The Mummy",
            MediaType = EnumItemType.Video,
            MediumInfo = new()
            {
                Id = 3,
                MediaType = EnumItemType.Video,
                Name = "Blu Ray"
            },
        };
        Items = [cd, book, bluRay];
        AllItems = [cd, book, bluRay];
        Mediums = [nameof(EnumItemType.All), nameof(EnumItemType.Book), nameof(EnumItemType.Music), nameof(EnumItemType.Video),];
    }

    public void AddEditItem()
    {
        const int startingItemCount = 3;
        var newItem = new MediaItem
        {
            Id = startingItemCount + AdditionalItemCount,
            Location = EnumLocationType.InCollection,
            MediaType = EnumItemType.Music,
            Name = $"CD {AdditionalItemCount}",
            MediumInfo = new()
            {
                Id = 1,
                MediaType = EnumItemType.Music,
                Name = "CD"
            },
        };
        AllItems.Add(newItem);
        Items.Add(newItem);
        AdditionalItemCount++;
    }

    public void DeleteItem()
    {
        AllItems.Remove(SelectedMediaItem);
        Items.Remove(SelectedMediaItem);
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
