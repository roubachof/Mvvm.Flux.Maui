﻿using System.Globalization;
using System.Resources;

namespace Mvvm.Flux.Maui.Localization
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly Type ResourceType = typeof(TranslateExtension); // TODO: Set the resx file name

        private readonly CultureInfo _cultureInfo = null;

        private ResourceManager _resourceManager;

        public TranslateExtension()
        {
            // The Old way
            // _cultureInfo = ServiceLocator.GetInstance<ILocalizer>().GetCurrentCultureInfo();

            _cultureInfo = CultureInfo.CurrentUICulture;
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            if (_resourceManager == null)
            {
                _resourceManager = new ResourceManager(ResourceType);
            }

            var translation = _resourceManager.GetString(Text, _cultureInfo);
            if (translation == null)
            {
#if RELEASE
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#else
                throw new InvalidOperationException(
                    $"Key '{Text}' was not found in resources '{ResourceType.Name}' for culture '{_cultureInfo.Name}'.");
#endif
            }

            return translation;
        }
    }
}