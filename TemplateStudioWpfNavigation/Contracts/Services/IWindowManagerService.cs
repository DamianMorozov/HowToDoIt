// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Contracts.Services;

public interface IWindowManagerService
{
	Window MainWindow { get; }

	void OpenInNewWindow(string pageKey, object parameter = null);

	bool? OpenInDialog(string pageKey, object parameter = null);

	Window GetWindow(string pageKey);
}