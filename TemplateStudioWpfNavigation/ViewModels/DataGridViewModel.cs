using System.Collections.ObjectModel;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using TemplateStudioWpfNavigation.Contracts.ViewModels;
using TemplateStudioWpfNavigation.Core.Contracts.Services;
using TemplateStudioWpfNavigation.Core.Models;

namespace TemplateStudioWpfNavigation.ViewModels
{
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
}
