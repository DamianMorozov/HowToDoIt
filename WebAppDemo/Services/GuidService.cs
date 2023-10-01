namespace WebAppDemo.Services;

public class GuidService : IGuidService
{
    #region Public and private fields, properties, constructor

    public Guid Value { get; } = Guid.NewGuid();

    #endregion
}