// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class WindowManagerService : IWindowManagerService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IPageService _pageService;

	public Window MainWindow
		=> Application.Current.MainWindow;

	public WindowManagerService(IServiceProvider serviceProvider, IPageService pageService)
	{
		_serviceProvider = serviceProvider;
		_pageService = pageService;
	}

	public void OpenInNewWindow(string key, object parameter = null)
	{
		Window window = GetWindow(key);
		if (window != null)
		{
			window.Activate();
		}
		else
		{
			window = new MetroWindow
			{
				Title = "TemplateStudioWpfNavigation",
				Style = Application.Current.FindResource("CustomMetroWindow") as Style
			};
			Frame frame = new()
			{
				Focusable = false,
				NavigationUIVisibility = NavigationUIVisibility.Hidden
			};

			window.Content = frame;
			Page page = _pageService.GetPage(key);
			window.Closed += OnWindowClosed;
			window.Show();
			frame.Navigated += OnNavigated;
			var navigated = frame.Navigate(page, parameter);
		}
	}

	public bool? OpenInDialog(string key, object parameter = null)
	{
		Window shellWindow = _serviceProvider.GetService(typeof(IShellDialogWindow)) as Window;
		Frame frame = ((IShellDialogWindow)shellWindow).GetDialogFrame();
		frame.Navigated += OnNavigated;
		shellWindow.Closed += OnWindowClosed;
		Page page = _pageService.GetPage(key);
		var navigated = frame.Navigate(page, parameter);
		return shellWindow.ShowDialog();
	}

	public Window GetWindow(string key)
	{
		foreach (Window window in Application.Current.Windows)
		{
			var dataContext = window.GetDataContext();
			if (dataContext?.GetType().FullName == key)
			{
				return window;
			}
		}

		return null;
	}

	private void OnNavigated(object sender, NavigationEventArgs e)
	{
		if (sender is Frame frame)
		{
			var dataContext = frame.GetDataContext();
			if (dataContext is INavigationAware navigationAware)
			{
				navigationAware.OnNavigatedTo(e.ExtraData);
			}
		}
	}

	private void OnWindowClosed(object sender, EventArgs e)
	{
		if (sender is Window window)
		{
			if (window.Content is Frame frame)
			{
				frame.Navigated -= OnNavigated;
			}

			window.Closed -= OnWindowClosed;
		}
	}
}