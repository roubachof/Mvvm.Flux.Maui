using MetroLog;
using MetroLog.Targets;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

using Prism;
using Prism.Ioc;

using Sharpnado.Tasks;

using Symbiot.Mobile.Domain.Lights;
using Symbiot.Mobile.Domain.Lights.Mock;
using Symbiot.Mobile.Infrastructure;
using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Presentation.Pages;
using Symbiot.Mobile.Presentation.Pages.Home;
using Symbiot.Mobile.Presentation.ViewModels;

using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

using LogLevel = MetroLog.LogLevel;

[assembly: ExportFont("fa_5_pro_solid.otf", Alias = "FontAwesome")]
[assembly: ExportFont("fa_5_pro_regular.otf", Alias = "FontAwesomeRegular")]

[assembly: ExportFont("OpenSans-Bold.ttf", Alias = "FontBold")]
[assembly: ExportFont("OpenSans-BoldItalic.ttf", Alias = "FontBoldItalic")]
[assembly: ExportFont("OpenSans-ExtraBold.ttf", Alias = "FontExtraBold")]
[assembly: ExportFont("OpenSans-Italic.ttf", Alias = "FontItalic")]
[assembly: ExportFont("OpenSans-Light.ttf", Alias = "FontLight")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "FontRegular")]
[assembly: ExportFont("OpenSans-SemiBold.ttf", Alias = "FontSemiBold")]

namespace Symbiot.Mobile
{
    public partial class App
    {
        private static ILogger log;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.TaskLoaderView.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(false, false);

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");
            if (result.Exception != null)
            {
                throw result.Exception;
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterInfrastructure(containerRegistry);

            RegisterDomain(containerRegistry);

            RegisterNavigation(containerRegistry);
        }

        private static void RegisterInfrastructure(IContainerRegistry containerRegistry)
        {
            InitializeAppCenter();

            log = InitializeLogger();

            containerRegistry.RegisterInstance<Prism.Logging.ILogger>(new PrismLoggerWrapper(log));
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
        }

        private static void RegisterDomain(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILightService, LightServiceMock>();
        }

        private static void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LightEditPage, LightEditPageViewModel>();
        }

        private static void InitializeAppCenter()
        {
#if RELEASE
            if (PlatformService.IsEmulator)
            {
                return;
            }

            Crashes.GetErrorAttachments = report =>
            {
                var compressedLogs = LoggerFactory.GetCompressedLogs();

                return new[]
                {
                    ErrorAttachmentLog.AttachmentWithBinary(
                        compressedLogs.ToArray(),
                        "logs.zip",
                        "application/x-zip-compressed"),
                };
            };

            AppCenter.Start(
                "ios=XXXXX;android=XXXXX",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Distribute));
#endif
        }

        private static ILogger InitializeLogger()
        {
            var config = new LoggingConfiguration();

#if RELEASE
            // Will be stored in: $"MetroLog{Path.DirectorySeparatorChar}MetroLogs{Path.DirectorySeparatorChar}Log.log"
            if (!PlatformService.IsEmulator)
            {
                config.AddTarget(LogLevel.Info, LogLevel.Fatal, new StreamingFileTarget { RetainDays = 2 });
            }
            else
            {
                config.AddTarget(LogLevel.Debug, LogLevel.Fatal, new DebugTarget());
            }
#else
            config.AddTarget(LogLevel.Debug, LogLevel.Fatal, new DebugTarget());
#endif

            LoggerFactory.Initialize(config);

            var logger = LoggerFactory.GetLogger(nameof(App));

            TaskMonitorConfiguration.ErrorHandler = (t, m, e) => logger.Error(m, e);

            return logger;
        }
    }
}
