// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.Views.Pages;

/// <summary>
/// Interaction logic for DataView.xaml
/// </summary>
public partial class DataPage : INavigableView<DataViewModel>
{
	public DataViewModel ViewModel
	{
		get;
	}

	public DataPage(DataViewModel viewModel)
	{
		ViewModel = viewModel;

		InitializeComponent();
	}
}