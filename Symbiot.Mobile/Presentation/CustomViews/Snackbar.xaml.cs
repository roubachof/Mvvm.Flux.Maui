
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Snackbar : Frame
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(Snackbar),
            propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(Snackbar));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(Snackbar),
            defaultValue: Color.Black);

        private static void TextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            
        }

        public static readonly BindableProperty TextSizeProperty = BindableProperty.Create(
            nameof(TextSize),
            typeof(double),
            typeof(Snackbar),
            defaultValue: 14d);

        public static readonly BindableProperty DisplayDurationMillisecondsProperty = BindableProperty.Create(
            nameof(DisplayDurationMilliseconds),
            typeof(int),
            typeof(Snackbar),
            defaultValue: 4000);

        public Snackbar()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double TextSize
        {
            get => (double)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        public int DisplayDurationMilliseconds
        {
            get => (int)GetValue(DisplayDurationMillisecondsProperty);
            set => SetValue(DisplayDurationMillisecondsProperty, value);
        }
    }
}