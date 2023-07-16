// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

public class ContentGridDetailViewModel : ObservableObject, INavigationAware
{
	private readonly ISampleDataService _sampleDataService;
	private SampleOrder _item;

	public SampleOrder Item
	{
		get { return _item; }
		set { SetProperty(ref _item, value); }
	}

	public ContentGridDetailViewModel(ISampleDataService sampleDataService)
	{
		_sampleDataService = sampleDataService;
	}

	public async void OnNavigatedTo(object parameter)
	{
		if (parameter is long orderID)
		{
			var data = await _sampleDataService.GetContentGridDataAsync();
			Item = data.First(i => i.OrderID == orderID);
		}
	}

	public void OnNavigatedFrom()
	{
	}
}