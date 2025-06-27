using SaisApp.ViewModels;

namespace SaisApp.Views;

public partial class AddApplicantPage : ContentPage
{
	public AddApplicantPage(AddApplicantViewModel viewModel)
	{
        InitializeComponent();
        viewModel.Navigation = this.Navigation; // <-- This is key
        BindingContext = viewModel;
    }


    private void FirstNameEntry_Completed(object sender, EventArgs e) => MiddleNameEntry.Focus();
    private void MiddleNameEntry_Completed(object sender, EventArgs e) => LastNameEntry.Focus();
    private void LastNameEntry_Completed(object sender, EventArgs e) => IdEntry.Focus();
    private void IdEntry_Completed(object sender, EventArgs e) => PostalEntry.Focus();
    private void PostalEntry_Completed(object sender, EventArgs e) => PhysicalEntry.Focus();

    //private void OfficerNameEntry_Completed(object sender, EventArgs e) => DesignationEntry.Focus();
    //private void DesignationEntry_Completed(object sender, EventArgs e) => SignatureEntry.Focus();
}