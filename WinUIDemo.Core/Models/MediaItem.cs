namespace WinUIDemo.Core.Models;

public class MediaItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EnumItemType MediaType { get; set; }
    public Medium MediumInfo { get; set; }
    public EnumLocationType Location { get; set; }
}