﻿using System;
using System.Globalization;

using Symbiot.Mobile.Infrastructure;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Symbiot.Mobile.Presentation.Converters
{
    public class TopSafeAreaToDoubleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !double.TryParse(parameter.ToString(), out var defaultValue))
            {
                defaultValue = 0;
            }

            return defaultValue + PlatformService.GetSafeArea().Top;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }

    public class BottomSafeAreaToDoubleConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !double.TryParse(parameter.ToString(), out var defaultValue))
            {
                defaultValue = 0;
            }

            return defaultValue + PlatformService.GetSafeArea().Bottom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
