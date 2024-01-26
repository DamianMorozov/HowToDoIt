namespace WebAppDemo.Models;

public sealed class AutoModel
{
	#region Public and private fields, properties, constructor

	public int Id { get; set; }
	public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
	public int EngineCapacity { get; set; }

    #endregion
}
