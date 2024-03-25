// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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