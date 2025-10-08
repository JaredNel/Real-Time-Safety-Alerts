using SafetyAlertsApp.ViewModels;

namespace SafetyAlertsApp.Views;

public partial class SafetyAlertsPage : ContentPage
{
    private readonly SafetyAlertsViewModel _viewModel;

    public SafetyAlertsPage(SafetyAlertsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAlertsCommand.ExecuteAsync(null);
    }
}
