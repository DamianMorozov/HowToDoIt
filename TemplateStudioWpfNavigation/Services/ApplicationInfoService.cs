// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TemplateStudioWpfNavigation.Services;

public class ApplicationInfoService : IApplicationInfoService
{
	public Version GetVersion()
	{
		// Set the app version in TemplateStudioWpfNavigation > Properties > Package > PackageVersion
		string assemblyLocation = Assembly.GetExecutingAssembly().Location;
		var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
		return new Version(version);
	}
}