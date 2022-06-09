using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using TemplateStudioWpfNavigation.Contracts.Services;
using TemplateStudioWpfNavigation.Contracts.ViewModels;
using TemplateStudioWpfNavigation.Core.Contracts.Services;
using TemplateStudioWpfNavigation.Core.Models;

namespace TemplateStudioWpfNavigation.ViewModels
{
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
}
