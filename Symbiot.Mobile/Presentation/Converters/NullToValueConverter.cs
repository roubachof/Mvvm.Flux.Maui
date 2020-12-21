using System;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.Converters
{
    public class NullToValueConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
            {
                return 0;
            }

            return double.Parse((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
