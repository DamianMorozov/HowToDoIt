namespace WinUIDemo.Core.Models;

public sealed class MediaItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EnumItemType MediaType { get; set; }
    public Medium MediumInfo { get; set; } = new();
    public EnumLocationType Location { get; set; }
}