namespace WebAppDemo.Abstractions;

public interface IProductService
{
    IEnumerable<ProductModel> GetAll();
}
