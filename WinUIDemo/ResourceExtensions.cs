namespace WinUIDemo;

public static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();
    public static string GetLocalized(this string resourceKey) => _resourceLoader.GetString(resourceKey);

    //public static ResourceLoader GetForCurrentView() => ResourceLoader.GetForCurrentView("Resources");
    //public static string GetApp() => GetForCurrentView().GetString("App");
}