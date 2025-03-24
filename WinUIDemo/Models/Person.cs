namespace WinUIDemo.Models;

public sealed partial class Person : ObservableRecipient
{
    [ObservableProperty]
    public partial string Name { get; set; }
    [ObservableProperty]
    public partial DateOnly BirthDay { get; set; }
}
