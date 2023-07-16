// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.TemplateSelectors;

public class MenuItemTemplateSelector : DataTemplateSelector
{
	public DataTemplate GlyphDataTemplate { get; set; }

	public DataTemplate ImageDataTemplate { get; set; }

	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		if (item is HamburgerMenuGlyphItem)
		{
			return GlyphDataTemplate;
		}

		if (item is HamburgerMenuImageItem)
		{
			return ImageDataTemplate;
		}

		return base.SelectTemplate(item, container);
	}
}