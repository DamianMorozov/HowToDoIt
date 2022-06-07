using System.Threading.Tasks;

namespace TemplateStudioWpfNavigation.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}
