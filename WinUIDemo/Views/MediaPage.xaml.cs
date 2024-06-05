namespace WinUIDemo.Views;

public sealed partial class MediaPage : Page
{
    private IList<MediaItem> _items = default!;
    private bool _isLoaded = false;
    private IList<string> _mediums = default!;
    private IList<MediaItem> _allItems = default!;

    public MediaViewModel ViewModel { get; }

    public MediaPage()
    {
        ViewModel = App.GetService<MediaViewModel>();
        InitializeComponent();
        ItemList.Loaded += ItemList_Loaded;
        ItemFilter.Loaded += ItemFilter_Loaded;
        Loaded += MediaPage_Loaded;
    }

    private void MediaPage_Loaded(object sender, RoutedEventArgs e)
    {
        ItemFilter.SelectionChanged += ItemFilter_SelectionChanged;
    }

    private void ItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var updatedItems = (
            from item in _allItems
            where string.IsNullOrWhiteSpace(ItemFilter.SelectedValue.ToString()) ||
                ItemFilter.SelectedValue.ToString() == nameof(EnumItemType.All) ||
                ItemFilter.SelectedValue.ToString() == item.MediaType.ToString()
            select item).ToList();
        ItemList.ItemsSource = updatedItems;
    }

    private void ItemList_Loaded(object sender, RoutedEventArgs e)
    {
        ListView listView = (ListView)sender;
        PopulateData();
        listView.ItemsSource = _items;
    }

    private void ItemFilter_Loaded(object sender, RoutedEventArgs e)
    {
        var filterCombo = (ComboBox)sender;
        PopulateData();
        filterCombo.ItemsSource = _mediums;
        filterCombo.SelectedIndex = 0;
    }

    private void PopulateData()
    {
        if (_isLoaded)
            return;
        _isLoaded = true;
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
        _items = [cd, book, bluRay];

        _allItems = [cd, book, bluRay];

        _mediums = [nameof(EnumItemType.All), nameof(EnumItemType.Book), nameof(EnumItemType.Music), nameof(EnumItemType.Video),];
    }
}
