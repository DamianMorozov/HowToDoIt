// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Behaviors;

public class ListViewItemSelectionBehavior : Behavior<ListView>
{
	public ICommand Command
	{
		get { return (ICommand)GetValue(CommandProperty); }
		set { SetValue(CommandProperty, value); }
	}

	public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ListViewItemSelectionBehavior), new PropertyMetadata(null));

	protected override void OnAttached()
	{
		base.OnAttached();
		ListView listView = AssociatedObject as ListView;
		listView.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
		listView.KeyDown += OnKeyDown;
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		ListView listView = AssociatedObject as ListView;
		listView.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
		listView.KeyDown -= OnKeyDown;
	}

	private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		=> SelectItem(e);

	private void OnKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Enter)
		{
			SelectItem(e);
			e.Handled = true;
		}
	}

	private void SelectItem(RoutedEventArgs args)
	{
		if (Command != null
		    && args.OriginalSource is FrameworkElement selectedItem
		    && Command.CanExecute(selectedItem.DataContext))
		{
			Command.Execute(selectedItem.DataContext);
		}
	}
}