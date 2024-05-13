using Microsoft.Extensions.Logging;
using Serilog;

namespace MauiApp1;

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
            });

        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
    //        .WriteTo.Debug()
            .WriteTo.Console()
            .CreateLogger();
        
        builder.Logging.AddSerilog(logger);
        builder.Services.AddTransient<MainPage>();

#if DEBUG
       // builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}