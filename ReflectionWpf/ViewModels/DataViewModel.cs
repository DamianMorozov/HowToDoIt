// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.ViewModels;

public partial class DataViewModel : ObservableObject, INavigationAware
{
	private bool _isInitialized = false;

	[ObservableProperty]
	private IEnumerable<DataColorModel> _colors;

	public void OnNavigatedTo()
	{
		if (!_isInitialized)
			InitializeViewModel();
	}

	public void OnNavigatedFrom()
	{
	}

	private void InitializeViewModel()
	{
		Random random = new();
		List<DataColorModel> colorCollection = new();

		for (int i = 0; i < 24; i++)
			colorCollection.Add(new()
			{
				Color = new SolidColorBrush(Color.FromArgb(
					(byte)200,
					(byte)random.Next(0, 250),
					(byte)random.Next(0, 250),
					(byte)random.Next(0, 250)))
			});

		Colors = colorCollection;

		_isInitialized = true;
	}
}