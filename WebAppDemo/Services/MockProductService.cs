// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebAppDemo.Services;

public sealed class MockProductService : IProductService
{
	#region Public and private fields, properties, constructor

	private readonly List<ProductModel> _products;

	public MockProductService()
	{
		Random random = new();
		_products = new();
		for (int i = 1; i < 13; i++)
		{
			_products.Add(new()
			{
				Id = i,
				Name = $"Item {i}",
				Price = random.Next(1_000_000),
				Dt = DateOnly.Parse($"2023-{i}-{i}", new DateTimeFormatInfo())
			});
		}
	}

	#endregion

	#region Public and private methods

	public IEnumerable<ProductModel> GetAll() => _products;

	#endregion
}