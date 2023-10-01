namespace WebAppDemo.Services;

public sealed class XmlAlertService : BaseService, IAlertService
{
    #region Public and private fields, properties, constructor

    public XmlAlertService(IWebHostEnvironment env)
    {
        var configurationBinder = new ConfigurationBuilder();
        configurationBinder.SetBasePath(env.ContentRootPath);
        configurationBinder.AddXmlFile("settings.xml");
        Config = configurationBinder.Build();
    }

    #endregion
}