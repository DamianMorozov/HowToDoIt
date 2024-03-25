// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class SystemService : ISystemService
{
	public void OpenInWebBrowser(string url)
	{
		// For more info see https://github.com/dotnet/corefx/issues/10361
		ProcessStartInfo psi = new()
		{
			FileName = url,
			UseShellExecute = true
		};
		Process.Start(psi);
	}
}