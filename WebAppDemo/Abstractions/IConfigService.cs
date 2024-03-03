namespace WebAppDemo.Abstractions;

public interface IConfigService
{
    ConfigModel Model { get; set; }
    string GetAlertMessage();
}