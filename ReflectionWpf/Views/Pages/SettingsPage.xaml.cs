// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.Views.Pages;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : INavigableView<SettingsViewModel>
{
	public SettingsViewModel ViewModel
	{
		get;
	}

	public SettingsPage(SettingsViewModel viewModel)
	{
		ViewModel = viewModel;

		InitializeComponent();
	}
}