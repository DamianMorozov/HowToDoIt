namespace WebAppDemo.Services;

public abstract class BaseConfigService
{
    #region Public and private fields, properties, constructor

    public virtual IConfiguration? Config { get; protected set; }
    public virtual ConfigModel Model { get; set; } = new();

	#endregion

	#region Public and private methods

	public virtual string GetAlertMessage() => Config?.GetSection("Alert")["Msg"] ?? string.Empty;

    #endregion
}