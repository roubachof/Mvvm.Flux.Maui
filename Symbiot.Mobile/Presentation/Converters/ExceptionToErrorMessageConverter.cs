using System;
using System.Globalization;

using Symbiot.Mobile.Infrastructure;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.Converters
{
    public class ExceptionToErrorMessageConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var exception = value as Exception;

            if (value == null)
            {
                return null;
            }

            return ApplicationExceptions.ToString(exception);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // One-Way converter only
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
