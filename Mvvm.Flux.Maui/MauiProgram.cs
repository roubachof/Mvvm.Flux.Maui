using MetroLog;
using MetroLog.Operators;
using MetroLog.Targets;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

using Mopups.Hosting;

using Mvvm.Flux.Maui.Domain.Lights;
using Mvvm.Flux.Maui.Domain.Lights.Mock;
using Mvvm.Flux.Maui.Infrastructure;
using Mvvm.Flux.Maui.Infrastructure.Logging;
using Mvvm.Flux.Maui.Presentation.Pages;
using Mvvm.Flux.Maui.Presentation.Pages.Home;

using Sharpnado.Tabs;
using Sharpnado.Tasks;
using SkiaSharp.Views.Maui.Controls.Hosting;
using LoggerFactory = MetroLog.LoggerFactory;
using LogLevel = MetroLog.LogLevel;

namespace Mvvm.Flux.Maui
{
    public static class MauiProgram
    {
        private static ILogger log;

        public static MauiApp CreateMauiApp()
        {
            Initialize();

            Sharpnado.TaskLoaderView.Initializer.Initialize(true, true);

            return MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseSharpnadoTabs(true)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "FontRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "FontSemiBold");
                    fonts.AddFont("OpenSans-Bold.ttf", "FontBold");
                    fonts.AddFont("OpenSans-ExtraBold.ttf", "FontExtraBold");
                    fonts.AddFont("fa_5_pro_solid.otf", "FontAwesome");
                    fonts.AddFont("fa_5_pro_regular.otf", "FontAwesomeRegular");
                })
                .ConfigureMopups()
                .UsePrism(prism =>
                {
                    prism.RegisterTypes(container =>
                    {
                        RegisterDomain(container);
                        RegisterNavigation(container);
                    });

                    prism.OnAppStart((navigation) => 
                        navigation.CreateBuilder().AddNavigationPage().AddSegment(nameof(MainPage)).NavigateAsync());
                })
                .Build();
        }

        private static void Initialize()
        {
            InitializeAppCenter();

            log = InitializeLogger();
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

            Crashes.GetErrorAttachments = (report) =>
            {
                var task = GetErrorAttachmentsAsync(report);
                task.RunSynchronously();
                return task.Result;
            };

            async Task<IEnumerable<ErrorAttachmentLog>> GetErrorAttachmentsAsync(ErrorReport report)
            {
                if (!LogOperatorRetriever.Instance.TryGetOperator<ILogCompressor>(out var logCompressor))
                {
                    return Array.Empty<ErrorAttachmentLog>();
                }

                var compressedLogs = await logCompressor.GetCompressedLogs();

                return new[]
                {
                    ErrorAttachmentLog.AttachmentWithBinary(
                        compressedLogs.ToArray(),
                        "logs.zip",
                        "application/x-zip-compressed"),
                };
            }

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
                config.AddTarget(LogLevel.Debug, LogLevel.Fatal, new TraceTarget());
            }
#else
            config.AddTarget(LogLevel.Debug, LogLevel.Fatal, new TraceTarget());
#endif

            LoggerFactory.Initialize(config);

            var logger = LoggerFactory.GetLogger(nameof(App));

            TaskMonitorConfiguration.ErrorHandler = (t, m, e) => logger.Error(m, e);

            return logger;
        }
    }
}
