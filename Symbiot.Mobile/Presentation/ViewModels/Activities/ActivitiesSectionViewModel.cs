using Prism.Navigation;

using Symbiot.Mobile.Localization;

namespace Symbiot.Mobile.Presentation.ViewModels.Activities
{
    public class ActivitiesSectionViewModel : ANavigableViewModel
    {
        public ActivitiesSectionViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = GlobalResources.Section_Activities_Title;
        }
    }
}