namespace WebAppDemo.Services;

public interface IMovieService
{
	IEnumerable<MovieModel> GetAll();
}
