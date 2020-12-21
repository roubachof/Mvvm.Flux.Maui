using System;
using System.Globalization;

using Symbiot.Mobile.Infrastructure;
using Symbiot.Mobile.Presentation.Styles;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.Converters
{
    public class ExceptionToImageSourceConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            switch (value)
            {
                case ServerException serverException:
                    return ResourcesHelper.GetResource<ImageSource>("IconLoadingServerErrorWhite");
                case NetworkException networkException:
                    return ResourcesHelper.GetResource<ImageSource>("IconLoadingInternetErrorWhite");
                default:
                    return ResourcesHelper.GetResource<ImageSource>("IconLoadingUnknownErrorWhite");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // One-Way converter only
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
