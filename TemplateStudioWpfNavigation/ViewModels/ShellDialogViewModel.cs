// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.ViewModels;

public class ShellDialogViewModel : ObservableObject
{
	private ICommand _closeCommand;

	public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(OnClose));

	public Action<bool?> SetResult { get; set; }

	private void OnClose()
	{
		bool result = true;
		SetResult(result);
	}
}