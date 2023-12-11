namespace WebAppDemo.Services;

public sealed class JsonConfigService : BaseConfigService, IConfigService
{
	#region Public and private fields, properties, constructor

	public JsonConfigService(IConfiguration configuration)
    {
        Config = configuration;
        Model.AlertMessage = GetAlertMessage();
    }

    #endregion
}