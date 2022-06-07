using System;
using System.Windows.Controls;

namespace TemplateStudioWpfNavigation.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        Page GetPage(string key);
    }
}
