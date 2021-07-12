using MetroLog;

using Prism.Navigation;

using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Localization;

namespace Symbiot.Mobile.Presentation.Pages.Scenarios
{
    public class ScenariosSectionViewModel : ANavigableViewModel
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(ScenariosSectionViewModel));

        public ScenariosSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = GlobalResources.Section_Scenarios_Title;
        }
    }
}