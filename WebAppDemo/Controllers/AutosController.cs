// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Controllers;

public sealed class AutosController : Controller
{
	#region Public and private fields, properties, constructor

	private readonly IAutoService _autoService;

	public AutosController(IAutoService autoService)
	{
		_autoService = autoService;
	}

	#endregion

	#region Public and private methods

	public ViewResult Index() => View(_autoService.GetAll());

	[ActionName("Cards")]
	public IActionResult GetList() => View(_autoService.GetAll());

	[NonAction]
	public ContentResult Name()
	{
		return Content("Item 1");
	}

	[ActionName("GetAll")]
	public IActionResult GetAll()
	{
		return View(_autoService.GetAll());
	}

	#endregion
}
