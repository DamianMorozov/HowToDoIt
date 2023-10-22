#pragma warning disable S125

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<JsonAlertService>();
builder.Services.AddSingleton<XmlAlertService>();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<UidService>();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();
//app.UseMiddleware<AgeMiddleware>();
app.UseRouting();
app.UseStaticFiles();
app.UseResponseCaching();
app.UseMvcWithDefaultRoute();

app.Run();
