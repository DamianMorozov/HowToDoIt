// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Helpers;

public static class WindowExtensions
{
	public static object GetDataContext(this Window window)
	{
		if (window.Content is Frame frame)
		{
			return frame.GetDataContext();
		}

		return null;
	}
}