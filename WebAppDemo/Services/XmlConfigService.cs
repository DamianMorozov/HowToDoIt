// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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