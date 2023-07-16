// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class PageService : IPageService
{
	private readonly Dictionary<string, Type> _pages = new();
	private readonly IServiceProvider _serviceProvider;

	public PageService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		Configure<MainViewModel, MainPage>();
		Configure<WebViewViewModel, WebViewPage>();
		Configure<ListDetailsViewModel, ListDetailsPage>();
		Configure<ContentGridViewModel, ContentGridPage>();
		Configure<ContentGridDetailViewModel, ContentGridDetailPage>();
		Configure<DataGridViewModel, DataGridPage>();
		Configure<SettingsViewModel, SettingsPage>();
	}

	public Type GetPageType(string key)
	{
		Type pageType;
		lock (_pages)
		{
			if (!_pages.TryGetValue(key, out pageType))
			{
				throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
			}
		}

		return pageType;
	}

	public Page GetPage(string key)
	{
		Type pageType = GetPageType(key);
		return _serviceProvider.GetService(pageType) as Page;
	}

	private void Configure<VM, V>()
		where VM : ObservableObject
		where V : Page
	{
		lock (_pages)
		{
			var key = typeof(VM).FullName;
			if (_pages.ContainsKey(key))
			{
				throw new ArgumentException($"The key {key} is already configured in PageService");
			}

			Type type = typeof(V);
			if (_pages.Any(p => p.Value == type))
			{
				throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
			}

			_pages.Add(key, type);
		}
	}
}