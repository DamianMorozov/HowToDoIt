// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Services;

public sealed class MockProductCategoryService : IProductCategoryService
{
	#region Public and private fields, properties, constructor

	private readonly List<ProductCategoryModel> _productCategories;

	public MockProductCategoryService()
	{
		_productCategories = new();
		_productCategories.Add(new()
		{
			Id = 1,
			Name = "Other",
		});
		_productCategories.Add(new()
		{
			Id = 2,
			Name = "Books",
		});
		_productCategories.Add(new()
		{
			Id = 2,
			Name = "Computer parts",
		});
		_productCategories.Add(new()
		{
			Id = 2,
			Name = "Cmartphones",
		});
	}

	#endregion

	#region Public and private methods

	public IEnumerable<ProductCategoryModel> GetAll() => _productCategories;

	#endregion
}