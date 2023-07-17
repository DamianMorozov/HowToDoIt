﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.ViewModels;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
	private bool _isInitialized = false;

	[ObservableProperty]
	private string _appVersion = string.Empty;

	[ObservableProperty]
	private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

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
		CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
		AppVersion = $"ReflectionWpf - {GetAssemblyVersion()}";

		_isInitialized = true;
	}

	private string GetAssemblyVersion()
	{
		return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
	}

	[RelayCommand]
	private void OnChangeTheme(string parameter)
	{
		switch (parameter)
		{
			case "theme_light":
				if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
					break;

				Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
				CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

				break;

			default:
				if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
					break;

				Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
				CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

				break;
		}
	}
}