namespace WebAppDemo.Abstractions;

public interface IMovieService
{
    IEnumerable<MovieModel> GetAll();
}
