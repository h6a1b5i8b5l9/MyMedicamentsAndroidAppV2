using Microsoft.Extensions.Logging;
using MauiApp1.Infrastructure.Database;
using MauiApp1.Views;
using System.IO;
using Microsoft.Maui.Storage;

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
                .Services.AddSingleton<IMedicamentRepository>(provider =>
                {
                    var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "medicaments.db3");
                    return new SqliteMedicamentRepository(dbPath);
                })
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
