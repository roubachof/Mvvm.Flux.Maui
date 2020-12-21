using Symbiot.Mobile.Localization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverlayView : Grid
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(OverlayView),
            defaultValue: GlobalResources.Common_Saving);

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(ImageSource),
            typeof(OverlayView));

        public OverlayView()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}