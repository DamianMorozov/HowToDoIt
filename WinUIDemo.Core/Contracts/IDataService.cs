namespace WinUIDemo.Core.Contracts;

public interface IDataService
{
    IList<MediaItem> GetItems();
    MediaItem GetItem(int id);
    int AddItem(MediaItem item);
    void UpdateItem(MediaItem item);
    IList<EnumItemType> GetItemTypes();
    Medium GetMedium(string name);
    IList<Medium> GetMediums();
    IList<Medium> GetMediums(EnumItemType itemType);
    IList<EnumLocationType> GetLocationTypes();
}