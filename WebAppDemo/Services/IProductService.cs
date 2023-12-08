namespace WebAppDemo.Services;

public interface IProductService
{
	IEnumerable<ProductModel> GetAll();
}
