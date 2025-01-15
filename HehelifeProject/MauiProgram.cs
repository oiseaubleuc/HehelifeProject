using Microsoft.Extensions.Logging;
using HehelifeProject.Data;
using HehelifeProject.Views;

namespace HehelifeProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("TahomaBold.ttf", "TahomaBold");
                });

            builder.Services.AddSingleton<CarService>();
            builder.Services.AddSingleton<InventoryPage>();
            builder.Services.AddSingleton<TitlePage>();
            builder.Services.AddSingleton<AddPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
