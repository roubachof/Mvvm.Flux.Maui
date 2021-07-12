using MetroLog;

using Symbiot.Mobile.Infrastructure.Logging;

using Xamarin.Forms;

namespace Symbiot.Mobile.Presentation.Pages
{
    public partial class MainPage : ContentPage
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(MainPage));

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}