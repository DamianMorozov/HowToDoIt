namespace WebAppDemo.Abstractions;

public interface IProductCategoryService
{
    IEnumerable<ProductCategoryModel> GetAll();
}
