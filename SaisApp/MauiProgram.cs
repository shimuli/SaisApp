using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SaisApp.Services;
using SaisApp.ViewModels;
using SaisApp.Views;

namespace SaisApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<IApplicantService, ApplicantService>();
        builder.Services.AddSingleton<ILookupService, LookupService>();

        builder.Services.AddTransient<AddApplicantPage>();
        builder.Services.AddTransient<AddApplicantViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
