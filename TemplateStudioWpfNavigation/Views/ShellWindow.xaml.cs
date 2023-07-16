// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
	public ShellWindow(ShellViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}

	public Frame GetNavigationFrame()
		=> shellFrame;

	public void ShowWindow()
		=> Show();

	public void CloseWindow()
		=> Close();
}