// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
	private readonly IFileService _fileService;
	private readonly AppConfig _appConfig;
	private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

	public PersistAndRestoreService(IFileService fileService, IOptions<AppConfig> appConfig)
	{
		_fileService = fileService;
		_appConfig = appConfig.Value;
	}

	public void PersistData()
	{
		if (App.Current.Properties != null)
		{
			var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
			var fileName = _appConfig.AppPropertiesFileName;
			_fileService.Save(folderPath, fileName, App.Current.Properties);
		}
	}

	public void RestoreData()
	{
		var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
		var fileName = _appConfig.AppPropertiesFileName;
		IDictionary properties = _fileService.Read<IDictionary>(folderPath, fileName);
		if (properties != null)
		{
			foreach (DictionaryEntry property in properties)
			{
				App.Current.Properties.Add(property.Key, property.Value);
			}
		}
	}
}