// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Views;

public partial class WebViewPage : Page
{
	private readonly WebViewViewModel _viewModel;

	public WebViewPage(WebViewViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
		_viewModel = viewModel;
		_viewModel.Initialize(webView);
	}

	private void OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		=> _viewModel.OnNavigationCompleted(sender, e);
}