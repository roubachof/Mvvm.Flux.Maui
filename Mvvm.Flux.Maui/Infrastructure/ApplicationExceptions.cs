using Mvvm.Flux.Maui.Localization;

namespace Mvvm.Flux.Maui.Infrastructure
{
    public static class ApplicationExceptions
    {
        public static string ToString(Exception exception)
        {
            switch (exception)
            {
                case ServerException:
                    return GlobalResources.Error_Server_Technical;
                case NetworkException:
                    return GlobalResources.Error_Network;
                case LocalizableException localizableException:
                    return localizableException.Message;
                default:
                    return GlobalResources.Error_Unhandled;
            }
        }
    }

    public class LocalizableException : Exception
    {
    }

    public class ServerException : Exception
    {
    }

    public class NetworkException : Exception
    {
    }
}
