// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Views;

public partial class ListDetailsPage : Page
{
	public ListDetailsPage(ListDetailsViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}
}