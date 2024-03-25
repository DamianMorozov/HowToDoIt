// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Controllers;

public sealed class MoviesController : Controller
{
	#region Public and private fields, properties, constructor

	private readonly IMovieService _movieService;

	public MoviesController(IMovieService movieService)
	{
		_movieService = movieService;
	}

	#endregion

	#region Public and private methods

	public ViewResult Index() => View(_movieService.GetAll());

	[ActionName("List")]
	public IActionResult GetList() => View(_movieService.GetAll());

	[NonAction]
	public ContentResult Name()
	{
		return Content("Item 1");
	}

	[ActionName("GetAll")]
	public IActionResult GetAll()
	{
		return View(_movieService.GetAll());
	}

	#endregion
}
