using SaisApp.Models;
using SaisApp.Services;
using SaisApp.Views;
using System.Collections.ObjectModel;

namespace SaisApp;

public partial class MainPage : ContentPage
{
    private readonly IApplicantService _applicantService;
    private readonly IServiceProvider _serviceProvider;
    public MainPage(IApplicantService applicantService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _applicantService = applicantService;
        _serviceProvider = serviceProvider;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // Start faded and disabled
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            RootLayout.Opacity = 0.3;
            RootLayout.IsEnabled = false;
            ApplicantsListView.IsVisible = false;

            var applicants = await _applicantService.GetApplicantsAsync();
            ApplicantsListView.ItemsSource = applicants;

            // Restore full opacity and enable interaction
            Loader.IsVisible = false;
            Loader.IsRunning = false;
            RootLayout.IsEnabled = true;
            ApplicantsListView.IsVisible = true;

            await RootLayout.FadeTo(1, 300); // Animate fade-in
        }
        catch (Exception ex)
        {
            Loader.IsVisible = false;
            await DisplayAlert("Error", $"Could not load applicants: {ex.Message}", "OK");
        }
    }

    private async void OnAddApplicantClicked(object sender, EventArgs e)
    {
        var addApplicantPage = _serviceProvider.GetRequiredService<AddApplicantPage>();
        await Navigation.PushAsync(addApplicantPage);
    }
}

