// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Services;

public sealed class HomeService : IHomeService
{
	#region Public and private fields, properties, constructor

	public HomeModel Model { get; set; } = new();

	#endregion

	#region Public and private methods

	public void Setup(StringValues userAgent)
	{
		Model.UserAgent = userAgent;
	}

	#endregion
}