namespace WebAppDemo.Services;

public sealed class JsonAlertService : BaseService, IAlertService
{
    #region Public and private fields, properties, constructor

    public JsonAlertService(IConfiguration configuration)
    {
        Config = configuration;
    }

    #endregion
}