// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.ViewModels;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
	private bool _isInitialized;

	[ObservableProperty]
	private string _appVersion = string.Empty;

	[ObservableProperty]
	private ThemeType _currentTheme = ThemeType.Unknown;

	public void OnNavigatedTo()
	{
		if (!_isInitialized)
			InitializeViewModel();
	}

	public void OnNavigatedFrom()
	{
	}

	private void InitializeViewModel()
	{
		CurrentTheme = Theme.GetAppTheme();
		AppVersion = $"ReflectionWpf - {GetAssemblyVersion()}";

		_isInitialized = true;
	}

	private string GetAssemblyVersion()
	{
		return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
	}

	[RelayCommand]
	private void OnChangeTheme(string parameter)
	{
		switch (parameter)
		{
			case "theme_light":
				if (CurrentTheme == ThemeType.Light)
					break;

				Theme.Apply(ThemeType.Light);
				CurrentTheme = ThemeType.Light;

				break;

			default:
				if (CurrentTheme == ThemeType.Dark)
					break;

				Theme.Apply(ThemeType.Dark);
				CurrentTheme = ThemeType.Dark;

				break;
		}
	}
}