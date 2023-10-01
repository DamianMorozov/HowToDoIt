namespace WebAppDemo.Services;

public abstract class BaseService
{
    #region Public and private fields, properties, constructor

    public virtual IConfiguration? Config { get; protected set; }

    #endregion

    #region Public and private methods

    public virtual string GetMessage() => Config?.GetSection("Alert")["Msg"] ?? string.Empty;

    #endregion
}