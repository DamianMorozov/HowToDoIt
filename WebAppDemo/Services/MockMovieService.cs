// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Services;

public sealed class MockMovieService : IMovieService
{
	#region Public and private fields, properties, constructor

	private readonly List<MovieModel> _movies;

	public MockMovieService()
	{
		Random random = new();
		_movies = new();
		for (int i = 1; i < 13; i++)
		{
			_movies.Add(new()
			{
				Id = i,
				Title = $"Title {i}",
				Genre = $"Genre {i}",
				Duration = random.Next(300),
			});
		}
	}

	#endregion

	#region Public and private methods

	public IEnumerable<MovieModel> GetAll() => _movies;

	#endregion
}
