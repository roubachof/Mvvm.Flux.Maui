using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using Prism;
using Prism.Ioc;

using Symbiot.Mobile.Infrastructure;

using Xamarin.Forms;

using Color = Android.Graphics.Color;

namespace Symbiot.Mobile.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            PlatformService.Initialize(
                IsRunningInEmulator(),
                Resources.DisplayMetrics.Density,
                (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density),
                (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density),
                () => new Thickness(0, 24, 0, 0));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat && Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                Window.SetFlags(WindowManagerFlags.TranslucentStatus, WindowManagerFlags.TranslucentStatus);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutFullscreen);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.SetStatusBarColor(Color.Transparent);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
            {
                Window.DecorView.SystemUiVisibility =
                    (StatusBarVisibility)((SystemUiFlags)Window.DecorView.SystemUiVisibility
                                          | SystemUiFlags.LightStatusBar);
            }

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Mr Gestures: generated from application name: 
            MR.Gestures.Android.Settings.LicenseKey = "XXXXXX";

            Android.Glide.Forms.Init(this);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        private static bool IsRunningInEmulator()
        {
            return (Build.Brand.StartsWith("generic") && Build.Device.StartsWith("generic"))
                   || Build.Fingerprint.StartsWith("generic")
                   || Build.Fingerprint.StartsWith("unknown")
                   || Build.Hardware.Contains("goldfish")
                   || Build.Hardware.Contains("ranchu")
                   || Build.Model.Contains("google_sdk")
                   || Build.Model.Contains("Emulator")
                   || Build.Model.Contains("Android SDK built for x86")
                   || Build.Manufacturer.Contains("Genymotion")
                   || Build.Product.Contains("sdk_google")
                   || Build.Product.Contains("google_sdk")
                   || Build.Product.Contains("sdk")
                   || Build.Product.Contains("sdk_x86")
                   || Build.Product.Contains("vbox86p")
                   || Build.Product.Contains("emulator")
                   || Build.Product.Contains("simulator");
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

