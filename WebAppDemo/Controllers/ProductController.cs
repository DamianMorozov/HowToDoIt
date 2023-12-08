namespace WebAppDemo.Controllers;

public sealed class ProductController : Controller
{
	#region Public and private fields, properties, constructor

	private readonly IProductService _productService;

	public ProductController(IProductService productService)
	{
		_productService = productService;
	}

	#endregion

	#region Public and private methods

	public ContentResult Name()
	{
		return Content("Item 1");
	}

	public ViewResult Index()
	{
		return View(_productService.GetAll());
	}

	#endregion
}
