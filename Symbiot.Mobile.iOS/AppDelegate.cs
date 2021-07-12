using Foundation;

using ObjCRuntime;

using Prism;
using Prism.Ioc;

using Symbiot.Mobile.Infrastructure;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView.iOS;

namespace Symbiot.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            PlatformService.Initialize(
                Runtime.Arch == Arch.SIMULATOR,
                UIScreen.MainScreen.Scale,
                (int)UIScreen.MainScreen.Bounds.Width,
                (int)UIScreen.MainScreen.Bounds.Height,
                SafeAreaGetter);

            Forms.Init();

            // Mr Gestures: generated from application name
            // MR.Gestures.iOS.Settings.LicenseKey = "XXXX";

            // Pancake
            PancakeViewRenderer.Init();

            // Sharpnado

#if RELEASE
            Xamarin.Forms.Nuke.FormsHandler.Init();
#endif
            Sharpnado.Tabs.iOS.Preserver.Preserve();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        private static Thickness SafeAreaGetter()
        {
            UIEdgeInsets safeArea;

            if (!UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                safeArea = new UIEdgeInsets(UIApplication.SharedApplication.StatusBarFrame.Size.Height, 0, 0, 0);
            }
            else if (UIApplication.SharedApplication.KeyWindow != null)
            {
                safeArea = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            }
            else if (UIApplication.SharedApplication.Windows.Length > 0)
            {
                safeArea = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
            }
            else
            {
                safeArea = UIEdgeInsets.Zero;
            }

            return new Thickness(
                safeArea.Left,
                safeArea.Top,
                safeArea.Right,
                safeArea.Bottom > 0 ? safeArea.Bottom - 10 : safeArea.Bottom);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
