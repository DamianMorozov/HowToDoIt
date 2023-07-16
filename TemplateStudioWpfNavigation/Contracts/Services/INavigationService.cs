// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Contracts.Services;

public interface INavigationService
{
	event EventHandler<string> Navigated;

	bool CanGoBack { get; }

	void Initialize(Frame shellFrame);

	bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false);

	void GoBack();

	void UnsubscribeNavigation();

	void CleanNavigation();
}