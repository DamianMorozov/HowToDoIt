﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

// TODO: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
public class SettingsViewModel : ObservableObject, INavigationAware
{
	private readonly AppConfig _appConfig;
	private readonly IThemeSelectorService _themeSelectorService;
	private readonly ISystemService _systemService;
	private readonly IApplicationInfoService _applicationInfoService;
	private AppTheme _theme;
	private string _versionDescription;
	private ICommand _setThemeCommand;
	private ICommand _privacyStatementCommand;

	public AppTheme Theme
	{
		get { return _theme; }
		set { SetProperty(ref _theme, value); }
	}

	public string VersionDescription
	{
		get { return _versionDescription; }
		set { SetProperty(ref _versionDescription, value); }
	}

	public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new RelayCommand<string>(OnSetTheme));

	public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new RelayCommand(OnPrivacyStatement));

	public SettingsViewModel(IOptions<AppConfig> appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService)
	{
		_appConfig = appConfig.Value;
		_themeSelectorService = themeSelectorService;
		_systemService = systemService;
		_applicationInfoService = applicationInfoService;
	}

	public void OnNavigatedTo(object parameter)
	{
		VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
		Theme = _themeSelectorService.GetCurrentTheme();
	}

	public void OnNavigatedFrom()
	{
	}

	private void OnSetTheme(string themeName)
	{
		AppTheme theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
		_themeSelectorService.SetTheme(theme);
	}

	private void OnPrivacyStatement()
		=> _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);
}