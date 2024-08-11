using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace SCsProjectMaster
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Preferences.Default.Set("ConnectionString", "data source = wdb2.hs-mittweida.de; initial catalog = jpfeifer; user id = jpfeifer; password = Naen^oyee9ah");
            Preferences.Default.Set("ServerVersion", "10.3.39-mariadb");
            // TODO: Use SecureStorage
            // await SecureStorage.Default.SetAsync("ConnectionString", "data source = wdb2.hs - mittweida.de; initial catalog = jpfeifer; user id = jpfeifer; password = Naen ^ oyee9ah");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
