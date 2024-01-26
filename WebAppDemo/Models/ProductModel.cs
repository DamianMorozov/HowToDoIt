namespace WebAppDemo.Models;

public sealed class ProductModel
{
    #region Public and private fields, properties, constructor

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
    public DateOnly Dt { get; set; }

    #endregion
}
