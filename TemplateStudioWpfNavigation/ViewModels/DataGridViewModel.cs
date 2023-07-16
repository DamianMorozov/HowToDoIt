// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

public class DataGridViewModel : ObservableObject, INavigationAware
{
	private readonly ISampleDataService _sampleDataService;

	public ObservableCollection<SampleOrder> Source { get; } = new();

	public DataGridViewModel(ISampleDataService sampleDataService)
	{
		_sampleDataService = sampleDataService;
	}

	public async void OnNavigatedTo(object parameter)
	{
		Source.Clear();

		// Replace this with your actual data
		var data = await _sampleDataService.GetGridDataAsync();

		foreach (SampleOrder item in data)
		{
			Source.Add(item);
		}
	}

	public void OnNavigatedFrom()
	{
	}
}