using Microsoft.Extensions.Logging;
using SafetyAlertsApp.Services;
using SafetyAlertsApp.ViewModels;
using SafetyAlertsApp.Views;

namespace SafetyAlertsApp;

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

		// Register Services
		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<ISafetyAlertService, SafetyAlertService>();
		builder.Services.AddSingleton<ILocationService, LocationService>();

		// Register ViewModels
		builder.Services.AddTransient<SafetyAlertsViewModel>();

		// Register Views
		builder.Services.AddTransient<SafetyAlertsPage>();

		return builder.Build();
	}
}
