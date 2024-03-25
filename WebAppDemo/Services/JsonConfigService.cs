// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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