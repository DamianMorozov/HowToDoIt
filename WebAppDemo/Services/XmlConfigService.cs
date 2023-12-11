namespace WebAppDemo.Services;

public sealed class XmlConfigService : BaseConfigService, IConfigService
{
    #region Public and private fields, properties, constructor

    public XmlConfigService(IWebHostEnvironment env)
    {
        var configurationBinder = new ConfigurationBuilder();
        configurationBinder.SetBasePath(env.ContentRootPath);
        configurationBinder.AddXmlFile("settings.xml");
        Config = configurationBinder.Build();
        
        Model.AlertMessage = GetAlertMessage();
	}

    #endregion
}