// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Controllers;

public sealed class HomeController : Controller
{
	#region Public and private fields, properties, constructor

	private readonly IHomeService _homeService;

	public HomeController(IHomeService homeService)
	{
		_homeService = homeService;
	}

	#endregion


	#region Public and private methods

	public ViewResult Index()
	{
		_homeService.Setup(HttpContext.Request.Headers["User-Agent"]);
		return View(_homeService.Model);
	}

	//public string Index2()
	//{
	//	StringBuilder html = new();

	//	IConfigService jsonService = HttpContext.RequestServices.GetRequiredService<JsonAlertService>();
	//	IConfigService xmlService = HttpContext.RequestServices.GetRequiredService<XmlAlertService>();
	//	//HttpContext.Response.ContentType = "text/html;charset=utf-8";

	//	//foreach (var serviceDescriptor in services)
	//	//{
	//	//    html.AppendLine($"Service type: {serviceDescriptor.ServiceType.FullName}");
	//	//    html.AppendLine($"Life time: {serviceDescriptor.Lifetime}");
	//	//    html.AppendLine($"Implementation type: {serviceDescriptor.ImplementationType?.FullName}");
	//	//}

	//	html.AppendLine($"Json service: {jsonService.GetMessage()}");
	//	html.AppendLine($"XML service: {xmlService.GetMessage()}");

	//	html.AppendLine(string.Empty);
	//	IGuidService guidService = HttpContext.RequestServices.GetRequiredService<IGuidService>();
	//	UidService uidService = HttpContext.RequestServices.GetRequiredService<UidService>();
	//	html.AppendLine($"{nameof(guidService)}: {guidService.Value}");
	//	html.AppendLine($"{nameof(uidService)}: {uidService.Guid.Value}");

	//	return html.ToString();
	//}

	#endregion
}