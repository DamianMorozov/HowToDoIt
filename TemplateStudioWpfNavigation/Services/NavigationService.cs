// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class NavigationService : INavigationService
{
	private readonly IPageService _pageService;
	private Frame _frame;
	private object _lastParameterUsed;

	public event EventHandler<string> Navigated;

	public bool CanGoBack => _frame.CanGoBack;

	public NavigationService(IPageService pageService)
	{
		_pageService = pageService;
	}

	public void Initialize(Frame shellFrame)
	{
		if (_frame == null)
		{
			_frame = shellFrame;
			_frame.Navigated += OnNavigated;
		}
	}

	public void UnsubscribeNavigation()
	{
		_frame.Navigated -= OnNavigated;
		_frame = null;
	}

	public void GoBack()
	{
		if (_frame.CanGoBack)
		{
			var vmBeforeNavigation = _frame.GetDataContext();
			_frame.GoBack();
			if (vmBeforeNavigation is INavigationAware navigationAware)
			{
				navigationAware.OnNavigatedFrom();
			}
		}
	}

	public bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false)
	{
		Type pageType = _pageService.GetPageType(pageKey);

		if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
		{
			_frame.Tag = clearNavigation;
			Page page = _pageService.GetPage(pageKey);
			var navigated = _frame.Navigate(page, parameter);
			if (navigated)
			{
				_lastParameterUsed = parameter;
				var dataContext = _frame.GetDataContext();
				if (dataContext is INavigationAware navigationAware)
				{
					navigationAware.OnNavigatedFrom();
				}
			}

			return navigated;
		}

		return false;
	}

	public void CleanNavigation()
		=> _frame.CleanNavigation();

	private void OnNavigated(object sender, NavigationEventArgs e)
	{
		if (sender is Frame frame)
		{
			bool clearNavigation = (bool)frame.Tag;
			if (clearNavigation)
			{
				frame.CleanNavigation();
			}

			var dataContext = frame.GetDataContext();
			if (dataContext is INavigationAware navigationAware)
			{
				navigationAware.OnNavigatedTo(e.ExtraData);
			}

			Navigated?.Invoke(sender, dataContext.GetType().FullName);
		}
	}
}