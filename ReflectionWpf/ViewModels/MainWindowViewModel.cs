// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace ReflectionWpf.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
	#region Public and private fields, properties, constructor

	private bool _isInitialized;

	[ObservableProperty]
	private string _applicationTitle = string.Empty;

	private string _controlsCount;
    public string ControlsCount
    {
        get => _controlsCount;
        set { _controlsCount = value; OnPropertyChanged(); }
    }

	// VS is bags here!
	//[ObservableProperty]
	//private List<ReflectionItemModel> _reflectionItems;

	private List<ReflectionItemModel> _reflectionItems;
	public List<ReflectionItemModel> ReflectionItems
	{
		get => _reflectionItems;
		set { _reflectionItems = value; OnPropertyChanged(); }
	}

	[ObservableProperty]
	private ObservableCollection<INavigationControl> _navigationItems = new();

	[ObservableProperty]
	private ObservableCollection<INavigationControl> _navigationFooter = new();

	[ObservableProperty]
	private ObservableCollection<MenuItem> _trayMenuItems = new();

	public MainWindowViewModel(INavigationService navigationService)
	{
		_reflectionItems = new(0);
		_controlsCount = $"Items count: {_reflectionItems.Count}";

		if (!_isInitialized)
			InitializeViewModel();
	}

	#endregion

	#region Public and private methods

	private void InitializeViewModel()
	{
		ApplicationTitle = "WPF UI - ReflectionWpf";

		NavigationItems = new()
		{
			new NavigationItem
			{
				Content = "Home",
				PageTag = "dashboard",
				Icon = SymbolRegular.Home24,
				PageType = typeof(DashboardPage)
			},
			new NavigationItem
			{
				Content = "Data",
				PageTag = "data",
				Icon = SymbolRegular.DataHistogram24,
				PageType = typeof(DataPage)
			}
		};

		NavigationFooter = new()
		{
			new NavigationItem
			{
				Content = "Settings",
				PageTag = "settings",
				Icon = SymbolRegular.Settings24,
				PageType = typeof(SettingsPage)
			}
		};

		TrayMenuItems = new()
		{
			new()
			{
				Header = "Home",
				Tag = "tray_home"
			}
		};

		_isInitialized = true;
	}

	[RelayCommand]
	public async Task OnClearUiElementsAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		DispatcherUtils.DispatcherUpdateApplication(() =>
		{
			ReflectionItems.Clear();
            ReflectionItems = new();
			ControlsCount = $"Items count: {ReflectionItems.Count}";
		});
	}

	[RelayCommand]
	public async Task OnGetUiElementsAsync()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

		DispatcherUtils.DispatcherUpdateApplication(() =>
		{
			ReflectionItems.Clear();
			ControlsCount = $"Items count: {ReflectionItems.Count}";
			if (Application.Current.MainWindow is not { } window) return;
			
			AddElementIntoList("MainWindow", window.Content);
			ReflectionItems = ReflectionItems
                .OrderBy(x => x.ControlTypeParent)
                .ThenBy(x => x.ControlType)
				.ThenBy(x => x.PropertyName)
				.ToList();
			
			ControlsCount = $"Items count: {ReflectionItems.Count}";
		});
	}

	private void AddElementIntoList(string parentName, object obj)
	{
		if (obj is not UIElement uiElement) return;
		Type t = uiElement.GetType();
		if (t.FullName is null) return;

        if (uiElement is Panel { Children: not null } panel)
        {
            foreach (UIElement gridChild in panel.Children)
            {
                AddElementIntoList($"{parentName}->{t.FullName}", gridChild);
            }
        }

        ReflectionItemModel item = new(parentName, t.FullName, string.Empty, string.Empty);
        ReflectionItems.Add(item);
        PropertyInfo[] properties = t.GetProperties();
        foreach (PropertyInfo property in properties)
        {
			ReflectionItemModel itemProperty = new(parentName, t.FullName, property.Name, 
                property.GetValue(uiElement, Array.Empty<object>())?.ToString() ?? string.Empty);
        ReflectionItems.Add(itemProperty);
        }
    }

	//private void GetElementInfo(List<ReflectionItemModel> list, FrameworkElement elementFramework)
	//       {
	//           Type t = typeof(FrameworkElement);
	//           BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
	//           MemberInfo[] members = t.GetMembers(flags);
	//           foreach (MemberInfo member in members)
	//           {
	//               string access = "";
	//               string stat = "";
	//		MethodBase? method = member as MethodBase;
	//               if (method != null)
	//               {
	//                   if (method.IsPublic) access = " Public";
	//                   else if (method.IsPrivate) access = " Private";
	//                   else if (method.IsFamily) access = " Protected";
	//                   else if (method.IsAssembly) access = " Internal";
	//                   else if (method.IsFamilyOrAssembly) access = " Protected Internal ";
	//                   if (method.IsStatic) stat = " Static";
	//               }
	//               //string output = $"/* {member.MemberType} */ {access}{stat} {member.DeclaringType} {member.Name}";
	//           }
	//           //DictionaryFromType(simpleClass);
	//       }

	#endregion
}