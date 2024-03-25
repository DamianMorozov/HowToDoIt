// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#pragma warning disable S125

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGuidService, GuidService>();
builder.Services.AddScoped<UidService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<JsonConfigService>();
builder.Services.AddScoped<XmlConfigService>();
builder.Services.AddScoped<IAutoService, MockAutoService>();
builder.Services.AddScoped<IMovieService, MockMovieService>();
builder.Services.AddScoped<IProductService, MockProductService>();
//builder.Services
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();
//app.UseMiddleware<AgeMiddleware>();
app.UseRouting();
app.UseStaticFiles();
app.UseResponseCaching();
app.UseMvcWithDefaultRoute();

app.Run();
