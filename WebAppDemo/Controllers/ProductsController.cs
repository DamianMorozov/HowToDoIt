namespace WebAppDemo.Controllers;

public sealed class ProductsController : Controller
{
	#region Public and private fields, properties, constructor

	private readonly IProductService _productService;

	public ProductsController(IProductService productService)
	{
		_productService = productService;
	}

	#endregion

	#region Public and private methods

	public ViewResult Index() => View(_productService.GetAll());

	[NonAction]
	public ContentResult Name()
	{
		return Content("Item 1");
	}

	[ActionName("GetAll")]
	public IActionResult GetAll()
	{
		return View(_productService.GetAll());
	}

	#endregion
}
