﻿using System;

using Xamarin.Forms;

namespace Symbiot.Mobile.Presentation.Styles
{
    public static class ResourcesHelper
    {
        public static T GetResource<T>(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            throw new InvalidOperationException($"key {key} not found in the resource dictionary");
        }

        public static Color GetResourceColor(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value))
            {
                return (Color)value;
            }

            throw new InvalidOperationException($"key {key} not found in the resource dictionary");
        }

        public static void SetDynamicResource(string targetResourceName, string sourceResourceName)
        {
            if (!Application.Current.Resources.TryGetValue(sourceResourceName, out var value))
            {
                throw new InvalidOperationException($"key {sourceResourceName} not found in the resource dictionary");
            }

            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDynamicResource<T>(string targetResourceName, T value)
        {
            Application.Current.Resources[targetResourceName] = value;
        }
    }
}
