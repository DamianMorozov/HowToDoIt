// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ReflectionWpf.Models;

public partial class ReflectionItemModel : ObservableObject
{
	#region Public and private fields, properties, constructor

	[ObservableProperty]
	private string _controlTypeParent;
	[ObservableProperty]
	private string _controlType;
	[ObservableProperty]
	private string _propertyName;
	[ObservableProperty]
	private string _propertyValue;

	
	public ReflectionItemModel(string controlTypeParent, string controlType, string propertyName, string propertyValue)
    {
        _controlTypeParent = controlTypeParent;
		_controlType = controlType;
		_propertyName = propertyName;
		_propertyValue = propertyValue;
	}

	#endregion
}