namespace WebAppDemo.Services;

public interface IHomeService
{
	HomeModel Model { get; set; }
	void Setup(StringValues userAgent);
}