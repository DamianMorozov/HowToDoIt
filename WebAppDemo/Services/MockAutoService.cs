namespace WebAppDemo.Services;

public sealed class MockAutoService : IAutoService
{
	#region Public and private fields, properties, constructor

	private readonly List<AutoModel> _autos;

	public MockAutoService()
	{
		Random random = new();
		_autos = new();
		for (int i = 1; i < 13; i++)
		{
			_autos.Add(new()
			{
				Id = i,
				Brand = $"Brand {i}",
				Model = $"Model {i}",
				EngineCapacity = random.Next(1_000),
			});
		}
	}

	#endregion

	#region Public and private methods

	public IEnumerable<AutoModel> GetAll() => _autos;

	#endregion
}