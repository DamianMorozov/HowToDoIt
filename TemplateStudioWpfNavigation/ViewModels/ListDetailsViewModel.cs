// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

public class ListDetailsViewModel : ObservableObject, INavigationAware
{
	private readonly ISampleDataService _sampleDataService;
	private SampleOrder _selected;

	public SampleOrder Selected
	{
		get { return _selected; }
		set { SetProperty(ref _selected, value); }
	}

	public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new();

	public ListDetailsViewModel(ISampleDataService sampleDataService)
	{
		_sampleDataService = sampleDataService;
	}

	public async void OnNavigatedTo(object parameter)
	{
		SampleItems.Clear();

		var data = await _sampleDataService.GetListDetailsDataAsync();

		foreach (SampleOrder item in data)
		{
			SampleItems.Add(item);
		}

		Selected = SampleItems.First();
	}

	public void OnNavigatedFrom()
	{
	}
}