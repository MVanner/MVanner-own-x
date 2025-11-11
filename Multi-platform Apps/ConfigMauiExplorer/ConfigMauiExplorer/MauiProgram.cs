using ConfigMauiExplorer.Interfaces;
using ConfigMauiExplorer.Services;
using Microsoft.Extensions.Logging;

namespace ConfigMauiExplorer;

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
        builder.Services.AddSingleton<IFolderPicker, FolderPicker>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}