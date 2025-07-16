using Microsoft.Extensions.Logging;
using MauiApp1.Infrastructure.Database;
using MauiApp1.Views;

namespace MauiApp1
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
                })
                .Services.AddSingleton<IMedicamentRepository, InMemoryMedicamentRepository>()
                .AddSingleton<MedicamentDatabaseService>()
                .AddTransient<AddMedicamentViewModel>()
                .AddTransient<MainPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
