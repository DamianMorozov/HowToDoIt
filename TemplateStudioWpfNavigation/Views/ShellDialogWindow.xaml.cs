// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Views;

public partial class ShellDialogWindow : MetroWindow, IShellDialogWindow
{
	public ShellDialogWindow(ShellDialogViewModel viewModel)
	{
		InitializeComponent();
		viewModel.SetResult = OnSetResult;
		DataContext = viewModel;
	}

	public Frame GetDialogFrame()
		=> dialogFrame;

	private void OnSetResult(bool? result)
	{
		DialogResult = result;
		Close();
	}
}