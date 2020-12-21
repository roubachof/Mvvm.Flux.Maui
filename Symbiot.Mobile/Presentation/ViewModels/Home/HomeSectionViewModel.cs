using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using MetroLog;

using Prism.Navigation;

using Sharpnado.Presentation.Forms;

using Symbiot.Mobile.Domain.Lights;
using Symbiot.Mobile.Infrastructure;
using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Localization;

namespace Symbiot.Mobile.Presentation.ViewModels.Home
{
    public class HomeSectionViewModel : ANavigableViewModel
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(HomeSectionViewModel));

        private readonly ILightService _lightService;

        public HomeSectionViewModel(INavigationService navigationService, ILightService lightService)
            : base(navigationService)
        {
            Log.Info("Building InterventionsViewModel");

            Title = GlobalResources.Section_Home_Title;

            _lightService = lightService;

            _lightService.LightUpdated += OnLightUpdated;

            Loader = new TaskLoaderNotifier<ObservableCollection<Light>>();

            NavigateToLightEditCommand = new TaskLoaderCommand<Light>(NavigateToLightEditAsync);
        }

        public TaskLoaderNotifier<ObservableCollection<Light>> Loader { get; }

        public ICommand NavigateToLightEditCommand { get; }

        public override void Destroy()
        {
            Log.Info($"Destroy()");
            _lightService.LightUpdated -= OnLightUpdated;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Log.Info("OnNavigatedTo()");
            AnalyticsHelper.TrackScreenDisplayed("HomeSection");

            if (Loader.IsNotStarted)
            {
                Loader.Load(LoadAsync);
            }
        }

        private async Task<ObservableCollection<Light>> LoadAsync()
        {
            Log.Info("LoadAsync()");

            var domainResult = await _lightService.GetLightsAsync();
            var result = new ObservableCollection<Light>(domainResult);
            Log.Info($"{result.Count} lights loaded");

            return result;
        }

        private void OnLightUpdated(object sender, Light light)
        {
            Log.Info($"OnLightUpdated( lightId: {light.Id} )");

            var itemList = Loader.Result;

            var matchingViewModel = itemList?.FirstOrDefault(item => item.Id == light.Id);
            if (matchingViewModel == null)
            {
                return;
            }

            int matchingViewModelIndex = itemList.IndexOf(matchingViewModel);
            itemList[matchingViewModelIndex] = light;
        }

        private Task NavigateToLightEditAsync(Light item)
        {
            Log.Info($"NavigateToLightEditAsync( id: {item.Id} )");

            var parameters = new NavigationParameters
                {
                    { nameof(Light.Id), item.Id },
                    { nameof(Title), item.Name },
                };

            return NavigationService.NavigateAsync("LightEditPage", parameters);
        }
    }
}