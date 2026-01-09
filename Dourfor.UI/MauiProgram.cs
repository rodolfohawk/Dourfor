using Microsoft.Extensions.Logging;
using Dourfor.UI.Services;
using Dourfor.UI.Pages;

namespace Dourfor.UI
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Configurar HttpClient
            builder.Services.AddSingleton(sp =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(Configuration.BackendUrl),
                    Timeout = TimeSpan.FromSeconds(30)
                };
                return httpClient;
            });

            // Registrar servicos
            builder.Services.AddSingleton<IAuthService, AuthService>();

            // Registrar paginas
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
