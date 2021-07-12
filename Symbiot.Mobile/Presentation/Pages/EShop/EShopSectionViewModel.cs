using MetroLog;

using Prism.Navigation;

using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Localization;

namespace Symbiot.Mobile.Presentation.Pages.EShop
{
    public class EShopSectionViewModel : ANavigableViewModel
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(EShopSectionViewModel));

        public EShopSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = GlobalResources.Section_EShop_Title;
        }
    }
}