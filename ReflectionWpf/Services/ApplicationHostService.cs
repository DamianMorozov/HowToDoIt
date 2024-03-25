﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.Services;

/// <summary>
/// Managed host of the application.
/// </summary>
public class ApplicationHostService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	private INavigationWindow _navigationWindow;

	public ApplicationHostService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	/// <summary>
	/// Triggered when the application host is ready to start the service.
	/// </summary>
	/// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		await HandleActivationAsync();
	}

	/// <summary>
	/// Triggered when the application host is performing a graceful shutdown.
	/// </summary>
	/// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
	public async Task StopAsync(CancellationToken cancellationToken)
	{
		await Task.CompletedTask;
	}

	/// <summary>
	/// Creates main window during activation.
	/// </summary>
	private async Task HandleActivationAsync()
	{
		await Task.CompletedTask;

		if (!Application.Current.Windows.OfType<MainWindow>().Any())
		{
			_navigationWindow = (_serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
			_navigationWindow!.ShowWindow();

			_navigationWindow.Navigate(typeof(DashboardPage));
		}

		await Task.CompletedTask;
	}
}