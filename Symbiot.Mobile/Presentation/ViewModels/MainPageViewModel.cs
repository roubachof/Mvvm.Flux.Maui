using MetroLog;

using Prism.Navigation;

using Symbiot.Mobile.Domain.Lights;
using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Presentation.Navigation;
using Symbiot.Mobile.Presentation.ViewModels.Activities;
using Symbiot.Mobile.Presentation.ViewModels.EShop;
using Symbiot.Mobile.Presentation.ViewModels.Home;
using Symbiot.Mobile.Presentation.ViewModels.Scenarios;

namespace Symbiot.Mobile.Presentation.ViewModels
{
    public class MainPageViewModel : ANavigableViewModel
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(MainPageViewModel));

        private readonly ANavigableViewModel[] _childrenViewModels = new ANavigableViewModel[4];

        private int _selectedViewModelIndex;

        public MainPageViewModel(INavigationService navigationService, ILightService lightService)
            : base(navigationService)
        {
            Log.Info("Building MainPageViewModel");

            _childrenViewModels[(int)MainSections.Home] =
                new HomeSectionViewModel(navigationService, lightService);

            _childrenViewModels[(int)MainSections.Scenarios] =
                new ScenariosSectionViewModel(navigationService);

            _childrenViewModels[(int)MainSections.EShop] =
                new EShopSectionViewModel(navigationService);

            _childrenViewModels[(int)MainSections.Activities] =
                new ActivitiesSectionViewModel(navigationService);
        }

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set
            {
                SetProperty(ref _selectedViewModelIndex, value);
                OnChildrenSelection(_selectedViewModelIndex);
            }
        }

        public HomeSectionViewModel HomeSection => (HomeSectionViewModel)_childrenViewModels[(int)MainSections.Home];

        public ScenariosSectionViewModel ScenariosSection => (ScenariosSectionViewModel)_childrenViewModels[(int)MainSections.Scenarios];

        public EShopSectionViewModel EShopSection => (EShopSectionViewModel)_childrenViewModels[(int)MainSections.EShop];

        public ActivitiesSectionViewModel ActivitiesSection => (ActivitiesSectionViewModel)_childrenViewModels[(int)MainSections.Activities];

        public override void Initialize(INavigationParameters parameters)
        {
            Log.Info($"Initialize( parameters: {parameters} )");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Log.Info($"OnNavigatedTo( parameters: {parameters} )");

            parameters.TryGetValue<MainSections>(NavigationKeys.SectionKey, out var targetSection);

            SelectedViewModelIndex = (int)targetSection;
        }

        public override void Destroy()
        {
            Log.Info("Destroy()");
            foreach (var child in _childrenViewModels)
            {
                child.Destroy();
            }
        }

        private void OnChildrenSelection(int childrenIndex, INavigationParameters parameters = null)
        {
            Log.Info($"OnChildrenSelection( childrenIndex: {childrenIndex} )");

            _childrenViewModels[childrenIndex].OnNavigatedTo(parameters);

            Title = _childrenViewModels[childrenIndex].Title;
        }
    }
}
