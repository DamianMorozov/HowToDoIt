#pragma warning disable S125

using System.Text;
using WebAppDemo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddSingleton<JsonAlertService>();
builder.Services.AddSingleton<XmlAlertService>();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<UidService>();
var app = builder.Build();
app.UseRouting();
app.UseResponseCaching();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        IAlertService jsonService = context.RequestServices.GetRequiredService<JsonAlertService>();
        IAlertService xmlService = context.RequestServices.GetRequiredService<XmlAlertService>();
        context.Response.ContentType = "text/html;charset=uti-f";

        var html = new StringBuilder();
        foreach (var serviceDescriptor in builder.Services)
        {
            html.AppendLine($"Service type: {serviceDescriptor.ServiceType.FullName}<br>");
            html.AppendLine($"Life time: {serviceDescriptor.Lifetime}<br>");
            html.AppendLine($"Implementation type: {serviceDescriptor.ImplementationType?.FullName}<br><hr>");
        }

        html.AppendLine($"Json service: {jsonService.GetMessage()}<br>");
        html.AppendLine($"XML service: {xmlService.GetMessage()}<br>");

        html.AppendLine("<hr>");
        IGuidService guidService = context.RequestServices.GetRequiredService<IGuidService>();
        UidService uidService = context.RequestServices.GetRequiredService<UidService>();
        html.AppendLine($"{nameof(guidService)}: {guidService.Value}<br>");
        html.AppendLine($"{nameof(uidService)}: {uidService.Guid.Value}<br>");
        
        await context.Response.WriteAsync(html.ToString());
    });
});

app.Run();
