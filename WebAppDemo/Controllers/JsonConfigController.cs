// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Controllers;

public sealed class JsonConfigController : Controller
{
	#region Public and private methods

	public ViewResult Index()
	{
		IConfigService configService = HttpContext.RequestServices.GetRequiredService<JsonConfigService>();
		return View(configService.Model);
	}

	//public string Index2()
	//{
	//	IConfigService xmlService = HttpContext.RequestServices.GetRequiredService<XmlAlertService>();
	//	//HttpContext.Response.ContentType = "text/html;charset=utf-8";

	//	//foreach (var serviceDescriptor in services)
	//	//{
	//	//    html.AppendLine($"Service type: {serviceDescriptor.ServiceType.FullName}");
	//	//    html.AppendLine($"Life time: {serviceDescriptor.Lifetime}");
	//	//    html.AppendLine($"Implementation type: {serviceDescriptor.ImplementationType?.FullName}");
	//	//}

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