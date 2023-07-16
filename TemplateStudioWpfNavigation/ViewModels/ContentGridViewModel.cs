// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

public class ContentGridViewModel : ObservableObject, INavigationAware
{
	private readonly INavigationService _navigationService;
	private readonly ISampleDataService _sampleDataService;
	private ICommand _navigateToDetailCommand;

	public ICommand NavigateToDetailCommand => _navigateToDetailCommand ?? (_navigateToDetailCommand = new RelayCommand<SampleOrder>(NavigateToDetail));

	public ObservableCollection<SampleOrder> Source { get; } = new();

	public ContentGridViewModel(ISampleDataService sampleDataService, INavigationService navigationService)
	{
		_sampleDataService = sampleDataService;
		_navigationService = navigationService;
	}

	public async void OnNavigatedTo(object parameter)
	{
		Source.Clear();

		// Replace this with your actual data
		var data = await _sampleDataService.GetContentGridDataAsync();
		foreach (SampleOrder item in data)
		{
			Source.Add(item);
		}
	}

	public void OnNavigatedFrom()
	{
	}

	private void NavigateToDetail(SampleOrder order)
	{
		_navigationService.NavigateTo(typeof(ContentGridDetailViewModel).FullName, order.OrderID);
	}
}