namespace WebAppDemo.Services;

public interface IConfigService
{
	ConfigModel Model { get; set; }
	string GetAlertMessage();
}