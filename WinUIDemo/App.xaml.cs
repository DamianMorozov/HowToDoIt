namespace WinUIDemo;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public sealed partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host { get; }
    public static T GetService<T>() where T : class =>
        (Current as App)!.Host.Services.GetService(typeof(T)) is not T service
            ? throw new ArgumentException(
                $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.")
            : service;
    public static WindowEx MainWindow { get; } = new MainWindow();
    public static UIElement? AppTitlebar { get; set; }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();
            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();
            // Services
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<INavigationViewService, NavigationViewService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            // Views and ViewModels
            services.AddTransient<ShellViewModel>();
            services.AddTransient<ShellPage>();
            services.AddTransient<MediaViewModel>();
            services.AddTransient<MediaPage>();
            services.AddTransient<CameraPreviewViewModel>();
            services.AddTransient<CameraPreviewPage>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<ItemDetailsViewModel>();
            services.AddTransient<ItemDetailsPage>();
            services.AddTransient<FusionCacheViewModel>();
            services.AddTransient<FusionCachePage>();
            // DI
            services.AddFusionCache()
                .WithDefaultEntryOptions(FusionCacheExtensions.GetFusionCacheEntryOptions());
            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    private async void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
        var dialog = new ContentDialog();
        //dialog.XamlRoot = this.XamlRoot;
        dialog.Style = Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = e.Message;
        dialog.Content = e.Exception.StackTrace;
        dialog.CloseButtonText = "Cancel";
        dialog.DefaultButton = ContentDialogButton.Close;
        _ = await dialog.ShowAsync();
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        //Frame rootFrame = Window.Current.Content as Frame;
        GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));
        await GetService<IActivationService>().ActivateAsync(args);
    }
}
