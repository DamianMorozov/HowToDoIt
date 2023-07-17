// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.Views.Pages;

/// <summary>
/// Interaction logic for DashboardPage.xaml
/// </summary>
public partial class DashboardPage : INavigableView<DashboardViewModel>
{
	public DashboardViewModel ViewModel
	{
		get;
	}

	public DashboardPage(DashboardViewModel viewModel)
	{
		ViewModel = viewModel;

		InitializeComponent();
	}
}