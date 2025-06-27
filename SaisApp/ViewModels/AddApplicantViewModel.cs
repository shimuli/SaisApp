using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SaisApp.Models;
using SaisApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Location = SaisApp.Models.Location;

namespace SaisApp.ViewModels;

public class AddApplicantViewModel : ObservableObject
{
    private readonly IApplicantService _applicantService;
    private readonly ILookupService _lookupService;
    private readonly IServiceProvider _serviceProvider;
    public INavigation Navigation { get; set; }
    public AddApplicantViewModel(IApplicantService applicantService, ILookupService lookupService, IServiceProvider serviceProvider)
    {
        _applicantService = applicantService;
        _lookupService = lookupService;

        LoadLookupData();
        _serviceProvider = serviceProvider;
    }

    public ObservableCollection<Genders> Genders { get; set; } = new();
    public ObservableCollection<MarriageStatus> MarriageStatus { get; set; } = new();
    public ObservableCollection<County> Counties { get; set; } = new();
    public ObservableCollection<SubCounty> SubCounties { get; set; } = new();
    public ObservableCollection<Location> Locations { get; set; } = new();
    public ObservableCollection<SubLocation> SubLocations { get; set; } = new();
    public ObservableCollection<Village> Villages { get; set; } = new();
    public ObservableCollection<ProgramsList> Programs { get; set; } = new();


    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    private async void LoadLookupData()
    {
        try
        {
            var genders = await _lookupService.GetGendersAsync();
            Genders.Clear();
            foreach (var item in genders)
                Genders.Add(item);

            List<MarriageStatus>? maritalStatus = await _lookupService.GetMaritualStatusListAsync();
            MarriageStatus.Clear();
            foreach (var item in maritalStatus)
                MarriageStatus.Add(item);

            var counties = await _lookupService.GetCountiesAsync();
            Counties.Clear();
            foreach (var item in counties)
                Counties.Add(item);

            var programs = await _lookupService.GetProgramsAsync();
            Programs.Clear();
            foreach (var item in programs)
                Programs.Add(item);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lookup load failed: {ex.Message}");
        }
    }

    public async Task LoadSubCounties(int countyId)
    {
        var subcounties = await _lookupService.GetSubCountiesAsync(countyId);
        SubCounties.Clear();
        foreach (var item in subcounties)
            SubCounties.Add(item);
    }

    public async Task LoadLocations(int subCountyId)
    {
        var locations = await _lookupService.GetLocationsAsync(subCountyId);
        Locations.Clear();
        foreach (var item in locations)
            Locations.Add(item);
    }

    public async Task LoadSubLocations(int locationId)
    {
        var subLocations = await _lookupService.GetSubLocationsAsync(locationId);
        SubLocations.Clear();
        foreach (var item in subLocations)
            SubLocations.Add(item);
    }

    public async Task LoadVillages(int subLocationId)
    {
        var villages = await _lookupService.GetVillagesAsync(subLocationId);
        Villages.Clear();
        foreach (var item in villages)
            Villages.Add(item);
    }

    // Cascading selection properties
    private County _selectedCounty;
    public County SelectedCounty
    {
        get => _selectedCounty;
        set
        {
            if (_selectedCounty != value)
            {
                _selectedCounty = value;
                OnPropertyChanged();
                if (_selectedCounty != null)
                    _ = LoadSubCounties(_selectedCounty.CountyId);

                SelectedSubCounty = null;
                SubCounties.Clear();
                Locations.Clear();
                SubLocations.Clear();
                Villages.Clear();
            }
        }
    }

    private SubCounty _selectedSubCounty;
    public SubCounty SelectedSubCounty
    {
        get => _selectedSubCounty;
        set
        {
            if (_selectedSubCounty != value)
            {
                _selectedSubCounty = value;
                OnPropertyChanged();
                if (_selectedSubCounty != null)
                    _ = LoadLocations(_selectedSubCounty.SubCountyId);

                SelectedLocation = null;
                Locations.Clear();
                SubLocations.Clear();
                Villages.Clear();
            }
        }
    }

    private Location _selectedLocation;
    public Location SelectedLocation
    {
        get => _selectedLocation;
        set
        {
            if (_selectedLocation != value)
            {
                _selectedLocation = value;
                OnPropertyChanged();
                if (_selectedLocation != null)
                    _ = LoadSubLocations(_selectedLocation.LocationId);

                SelectedSubLocation = null;
                SubLocations.Clear();
                Villages.Clear();
            }
        }
    }

    private SubLocation _selectedSubLocation;
    public SubLocation SelectedSubLocation
    {
        get => _selectedSubLocation;
        set
        {
            if (_selectedSubLocation != value)
            {
                _selectedSubLocation = value;
                OnPropertyChanged();
                if (_selectedSubLocation != null)
                    _ = LoadVillages(_selectedSubLocation.SubLocationId);

                SelectedVillage = null;
                Villages.Clear();
            }
        }
    }

    private Village _selectedVillage;
    public Village SelectedVillage
    {
        get => _selectedVillage;
        set
        {
            if (_selectedVillage != value)
            {
                _selectedVillage = value;
                OnPropertyChanged();
            }
        }
    }

    // Applicant form fields
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string IdNumber { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Today;
    public string PostalAddress { get; set; }
    public string PhysicalAddress { get; set; }

    public Genders SelectedGender { get; set; }
    public MarriageStatus SelectedMaritalStatus { get; set; }

    public List<ProgramsList> SelectedPrograms { get; set; } = new();
    public string PhoneNumbersRaw { get; set; }

    // Official approval
    public string OfficerName { get; set; }
    public string Designation { get; set; }
    public string Signature { get; set; }

    public bool DeclarationChecked { get; set; }

    // Submit command
    public Command SubmitCommand => new(async () => await SubmitApplicantAsync());

    private async Task SubmitApplicantAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        var validationErrors = ValidateFields();

        if (validationErrors.Any())
        {
            var message = string.Join("\n", validationErrors);
            await Application.Current.MainPage.DisplayAlert("Validation Error", message, "OK");

            IsBusy = false;
            return;
        }

        var request = new CreateApplicantRequest
        {
            createApplicant = new CreateApplicant
            {
                firstName = FirstName,
                middleName = MiddleName,
                lastName = LastName,
                idNumber = IdNumber,
                dateOfBirth = DateOfBirth,
                postalAddress = PostalAddress,
                physicalAddress = PhysicalAddress,
                genderId = SelectedGender?.Id ?? 0,
                maritalStatusId = SelectedMaritalStatus?.MaritalStatusId ?? 0,
                countyId = SelectedCounty?.CountyId ?? 0,
                subCountyId = SelectedSubCounty?.SubCountyId ?? 0,
                locationId = SelectedLocation?.LocationId ?? 0,
                subLocationId = SelectedSubLocation?.SubLocationId ?? 0,
                villageId = SelectedVillage?.VillageId ?? 0,
                formStatus = "Submitted",
                declarationChecked = true,
                dataEnteredBy = "system_user",
            },
            phoneNumbersRaw = PhoneNumbersRaw,
            selectedProgrammeIds = Programs.Where(p => p.IsSelected).Select(p => p.ProgrammeId).ToList(),
            officialApproval = new OfficialApproval
            {
                officerName = "-",
                designation = "-",
                signature = "-"
            }
        };

        var result = await _applicantService.AddApplicantAsync(request);

        if (result)
        {
            IsBusy = false;
            Application.Current.MainPage = new NavigationPage(_serviceProvider.GetService<MainPage>());
        }
        else
        {
            IsBusy = false;
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to add applicant", "OK");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    private List<string> ValidateFields()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(FirstName))
            errors.Add("First name is required.");

        if (string.IsNullOrWhiteSpace(MiddleName))
            errors.Add("Middle name is required.");

        if (string.IsNullOrWhiteSpace(LastName))
            errors.Add("Last name is required.");

        if (string.IsNullOrWhiteSpace(IdNumber))
            errors.Add("ID number is required.");

        if (SelectedGender == null)
            errors.Add("Gender is required.");

        if (SelectedMaritalStatus == null)
            errors.Add("Marital status is required.");

        if (string.IsNullOrWhiteSpace(PostalAddress))
            errors.Add("Postal address is required.");

        if (string.IsNullOrWhiteSpace(PhysicalAddress))
            errors.Add("Physical address is required.");

        if (SelectedCounty == null)
            errors.Add("County is required.");

        if (SelectedSubCounty == null)
            errors.Add("SubCounty is required.");

        if (SelectedLocation == null)
            errors.Add("Location is required.");

        if (SelectedSubLocation == null)
            errors.Add("SubLocation is required.");

        if (SelectedVillage == null)
            errors.Add("Village is required.");

        if (!Programs.Any(p => p.IsSelected))
            errors.Add("At least one program must be selected.");

        if (string.IsNullOrWhiteSpace(PhoneNumbersRaw))
            errors.Add("Phone numbers are required.");

        if (!DeclarationChecked)
            errors.Add("Declaration must be accepted.");

        return errors;
    }

}
