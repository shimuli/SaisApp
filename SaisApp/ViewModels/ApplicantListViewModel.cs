using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaisApp.Models;
using SaisApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaisApp.ViewModels;

public class ApplicantListViewModel : ObservableObject
{
    public ObservableCollection<Applicant> Applicants { get; } = new();
    public AsyncRelayCommand LoadCommand { get; }
    //public RelayCommand<Applicant> EditCommand { get; }
    //public RelayCommand<int> DeleteCommand { get; }

    public ApplicantListViewModel()
    {
        var svc = new ApplicantService();
        LoadCommand = new AsyncRelayCommand(async () => {
            Applicants.Clear();
            var list = await svc.GetApplicantsAsync();
            foreach (var a in list) Applicants.Add(a);
        });
        // Add edit and delete logic
    }
}