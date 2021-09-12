using System.Threading.Tasks;

using MetroLog;

using Prism.Navigation;

using Sharpnado.Presentation.Forms;

using Symbiot.Mobile.Domain.Lights;
using Symbiot.Mobile.Infrastructure;
using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Localization;

using Xamarin.Forms;

namespace Symbiot.Mobile.Presentation.Pages.Home
{
    public class LightEditPageViewModel : ANavigableViewModel
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(LightEditPageViewModel));

        private readonly ILightService _lightService;

        private int _lightId;

        public LightEditPageViewModel(INavigationService navigationService, ILightService lightService)
            : base(navigationService)
        {
            Log.Info("Building LightEditPageViewModel");

            Title = GlobalResources.Section_Home_Title;

            _lightService = lightService;

            Loader = new TaskLoaderNotifier<LightViewModel>();

            SaveCommand = new TaskLoaderCommand(SaveAsync, autoRaiseCanExecuteChange: true);
            ToggleLightCommand = new Command(ToggleLight);

            ActionOneCommand = new TaskLoaderCommand(
                async () =>
                    {
                        await Task.Delay(200);
                        throw new NetworkException();
                    });

            ActionTwoCommand = new TaskLoaderCommand(
                async () =>
                    {
                        await Task.Delay(200);
                        throw new ServerException();
                    });

            CompositeCommandLoader = new CompositeTaskLoaderNotifier(
                SaveCommand.Notifier,
                ActionOneCommand.Notifier,
                ActionTwoCommand.Notifier);
        }

        public TaskLoaderNotifier<LightViewModel> Loader { get; }

        public CompositeTaskLoaderNotifier CompositeCommandLoader { get; }

        public TaskLoaderCommand SaveCommand { get; }

        public Command ToggleLightCommand { get; }

        public TaskLoaderCommand ActionOneCommand { get; }

        public TaskLoaderCommand ActionTwoCommand { get; }

        public override void Initialize(INavigationParameters parameters)
        {
            Log.Info($"Initialize( parameters: {parameters} )");

            Title = parameters.GetValue<string>(nameof(Title));
            _lightId = parameters.GetValue<int>(nameof(Light.Id));

            Loader.Load(_ => LoadAsync(_lightId));
        }

        public async Task<LightViewModel> LoadAsync(int lightId)
        {
            Log.Info($"LoadAsync( lightId: {lightId} )");

            var entity = await _lightService.GetLightAsync(lightId);
            return new LightViewModel(entity);
        }

        private void ToggleLight()
        {
            Loader.Result.IsOn = !Loader.Result.IsOn;
        }

        private async Task SaveAsync()
        {
            Log.Info("SaveAsync()");

            await _lightService.UpdateLightAsync(Loader.Result.GetEntity());
            await NavigationService.GoBackAsync();
        }
    }
}