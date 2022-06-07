using System;
using System.Diagnostics;
using System.Reflection;

using TemplateStudioWpfNavigation.Contracts.Services;

namespace TemplateStudioWpfNavigation.Services
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        public ApplicationInfoService()
        {
        }

        public Version GetVersion()
        {
            // Set the app version in TemplateStudioWpfNavigation > Properties > Package > PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version);
        }
    }
}
