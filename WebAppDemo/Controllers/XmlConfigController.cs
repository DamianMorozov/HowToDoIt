// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Controllers;

public sealed class XmlConfigController : Controller
{
	#region Public and private methods

	public ViewResult Index()
	{
		IConfigService configService = HttpContext.RequestServices.GetRequiredService<XmlConfigService>();
		return View(configService.Model);
	}

	//public string Index2()
	//{
	//	//foreach (var serviceDescriptor in services)
	//	//{
	//	//    html.AppendLine($"Service type: {serviceDescriptor.ServiceType.FullName}");
	//	//    html.AppendLine($"Life time: {serviceDescriptor.Lifetime}");
	//	//    html.AppendLine($"Implementation type: {serviceDescriptor.ImplementationType?.FullName}");
	//	//}

	//	IGuidService guidService = HttpContext.RequestServices.GetRequiredService<IGuidService>();
	//	UidService uidService = HttpContext.RequestServices.GetRequiredService<UidService>();
	//	html.AppendLine($"{nameof(guidService)}: {guidService.Value}");
	//	html.AppendLine($"{nameof(uidService)}: {uidService.Guid.Value}");

	//	return html.ToString();
	//}

	#endregion
}