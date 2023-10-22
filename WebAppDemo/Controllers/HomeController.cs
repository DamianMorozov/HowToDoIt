namespace WebAppDemo.Controllers;

public class HomeController : Controller
{
    #region Public and private methods

    public string Index()
    {
        var userAgent = HttpContext.Request.Headers["User-Agent"];
        var html = new StringBuilder();
        html.AppendLine($"{nameof(userAgent)}: {userAgent}");

        IAlertService jsonService = HttpContext.RequestServices.GetRequiredService<JsonAlertService>();
        IAlertService xmlService = HttpContext.RequestServices.GetRequiredService<XmlAlertService>();
        //HttpContext.Response.ContentType = "text/html;charset=utf-8";

        //foreach (var serviceDescriptor in services)
        //{
        //    html.AppendLine($"Service type: {serviceDescriptor.ServiceType.FullName}");
        //    html.AppendLine($"Life time: {serviceDescriptor.Lifetime}");
        //    html.AppendLine($"Implementation type: {serviceDescriptor.ImplementationType?.FullName}");
        //}

        html.AppendLine($"Json service: {jsonService.GetMessage()}");
        html.AppendLine($"XML service: {xmlService.GetMessage()}");

        html.AppendLine(string.Empty);
        IGuidService guidService = HttpContext.RequestServices.GetRequiredService<IGuidService>();
        UidService uidService = HttpContext.RequestServices.GetRequiredService<UidService>();
        html.AppendLine($"{nameof(guidService)}: {guidService.Value}");
        html.AppendLine($"{nameof(uidService)}: {uidService.Guid.Value}");

        return html.ToString();
    }

    #endregion
}
