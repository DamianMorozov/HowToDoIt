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