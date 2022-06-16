// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://dotnet.microsoft.com/en-us/learn/maui/first-app-tutorial/
// https://docs.microsoft.com/en-us/dotnet/maui/get-started/first-app?pivots=devices-windows

namespace NetMauiDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("CascadiaCode-ExtraLight.ttf", "CascadiaCode-ExtraLight");
                    fonts.AddFont("OpenSans-Regular.ttf", "CascadiaCode-ExtraLight");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}