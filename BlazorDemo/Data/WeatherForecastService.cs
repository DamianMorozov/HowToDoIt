// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDemo.Data;

public class WeatherForecastService
{
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
	{
		return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = startDate.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray());
	}
}