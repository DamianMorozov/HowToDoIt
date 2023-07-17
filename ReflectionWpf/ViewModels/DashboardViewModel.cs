// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.ViewModels;

public partial class DashboardViewModel : ObservableObject, INavigationAware
{
	[ObservableProperty]
	private int _counter = 0;

	public void OnNavigatedTo()
	{
	}

	public void OnNavigatedFrom()
	{
	}

	[RelayCommand]
	private void OnCounterIncrement()
	{
		Counter++;
	}
}