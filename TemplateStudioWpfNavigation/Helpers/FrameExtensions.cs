// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Helpers;

public static class FrameExtensions
{
	public static object GetDataContext(this Frame frame)
	{
		if (frame.Content is FrameworkElement element)
		{
			return element.DataContext;
		}

		return null;
	}

	public static void CleanNavigation(this Frame frame)
	{
		while (frame.CanGoBack)
		{
			frame.RemoveBackEntry();
		}
	}
}