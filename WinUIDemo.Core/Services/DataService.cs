namespace WinUIDemo.Core.Services;

public sealed class DataService : IDataService
{
    private IList<MediaItem> _items;
    private IList<EnumItemType> _itemTypes;
    private IList<Medium> _mediums;
    private IList<EnumLocationType> _locationTypes;

    public DataService()
    {
        PopulateItemTypes();
        PopulateMediums();
        PopulateLocationTypes();
        PopulateItems();
    }

    private void PopulateItems()
    {
        var cd = new MediaItem
        {
            Id = 1,
            Name = "Classical Favorites",
            MediaType = EnumItemType.Music,
            MediumInfo = _mediums.FirstOrDefault(m => m.Name == "CD"),
            Location = EnumLocationType.InCollection
        };

        var book = new MediaItem
        {
            Id = 2,
            Name = "Classic Fairy Tales",
            MediaType = EnumItemType.Book,
            MediumInfo = _mediums.FirstOrDefault(m => m.Name == "Hardcover"),
            Location = EnumLocationType.InCollection
        };

        var bluRay = new MediaItem
        {
            Id = 3,
            Name = "The Mummy",
            MediaType = EnumItemType.Video,
            MediumInfo = _mediums.FirstOrDefault(m => m.Name == "Blu Ray"),
            Location = EnumLocationType.InCollection
        };

        _items = new List<MediaItem>
        {
            cd,
            book,
            bluRay
        };
    }

    private void PopulateMediums()
    {
        var cd = new Medium { Id = 1, MediaType = EnumItemType.Music, Name = "CD" };
        var vinyl = new Medium { Id = 2, MediaType = EnumItemType.Music, Name = "Vinyl" };
        var hardcover = new Medium { Id = 3, MediaType = EnumItemType.Book, Name = "Hardcover" };
        var paperback = new Medium { Id = 4, MediaType = EnumItemType.Book, Name = "Paperback" };
        var dvd = new Medium { Id = 5, MediaType = EnumItemType.Video, Name = "DVD" };
        var bluRay = new Medium { Id = 6, MediaType = EnumItemType.Video, Name = "Blu Ray" };

        _mediums = new List<Medium>
        {
            cd,
            vinyl,
            hardcover,
            paperback,
            dvd,
            bluRay
        };
    }

    private void PopulateItemTypes()
    {
        _itemTypes = new List<EnumItemType>
        {
            EnumItemType.Book,
            EnumItemType.Music,
            EnumItemType.Video
        };
    }

    private void PopulateLocationTypes()
    {
        _locationTypes = new List<EnumLocationType>
        {
            EnumLocationType.InCollection,
            EnumLocationType.Loaned
        };
    }

    public int AddItem(MediaItem item)
    {
        item.Id = _items.Max(i => i.Id) + 1;
        _items.Add(item);

        return item.Id;
    }

    public MediaItem GetItem(int id)
    {
        return _items.FirstOrDefault(i => i.Id == id);
    }

    public IList<MediaItem> GetItems()
    {
        return _items;
    }

    public IList<EnumItemType> GetItemTypes()
    {
        return _itemTypes;
    }

    public IList<Medium> GetMediums()
    {
        return _mediums;
    }

    public IList<Medium> GetMediums(EnumItemType itemType)
    {
        return _mediums
            .Where(m => m.MediaType == itemType)
            .ToList();
    }

    public IList<EnumLocationType> GetLocationTypes()
    {
        return _locationTypes;
    }

    public void UpdateItem(MediaItem item)
    {
        var idx = -1;
        var matchedItem =
            (from x in _items
                let ind = idx++
                where x.Id == item.Id
                select ind).FirstOrDefault();

        if (idx == -1)
        {
            throw new Exception("Unable to update item. Item not found in collection.");
        }

        _items[idx] = item;
    }

    public Medium GetMedium(string name)
    {
        return _mediums.FirstOrDefault(m => m.Name == name);
    }
}