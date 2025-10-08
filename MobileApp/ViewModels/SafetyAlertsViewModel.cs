using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SafetyAlertsApp.Models;
using SafetyAlertsApp.Services;

namespace SafetyAlertsApp.ViewModels;

public partial class SafetyAlertsViewModel : ObservableObject
{
    private readonly ISafetyAlertService _alertService;
    private readonly ILocationService _locationService;

    [ObservableProperty]
    private ObservableCollection<SafetyAlert> _alerts = new();

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = "Loading alerts...";

    [ObservableProperty]
    private SafetyAlert? _selectedAlert;

    public SafetyAlertsViewModel(ISafetyAlertService alertService, ILocationService locationService)
    {
        _alertService = alertService;
        _locationService = locationService;
    }

    [RelayCommand]
    private async Task LoadAlertsAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            StatusMessage = "Loading alerts...";

            var alerts = await _alertService.GetAlertsAsync();
            
            // Get user location to calculate distances
            var location = await _locationService.GetCurrentLocationAsync();
            
            if (location != null)
            {
                foreach (var alert in alerts)
                {
                    var distance = CalculateDistance(
                        location.Latitude, 
                        location.Longitude,
                        alert.Location.Latitude,
                        alert.Location.Longitude);
                    alert.DistanceFromUser = Math.Round(distance, 1);
                }

                // Sort by distance
                alerts = alerts.OrderBy(a => a.DistanceFromUser).ToList();
            }

            Alerts.Clear();
            foreach (var alert in alerts)
            {
                Alerts.Add(alert);
            }

            StatusMessage = $"{Alerts.Count} alerts found";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading alerts: {ex.Message}";
            Console.WriteLine($"Error in LoadAlertsAsync: {ex}");
        }
        finally
        {
            IsLoading = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAlertsAsync()
    {
        IsRefreshing = true;
        await LoadAlertsAsync();
    }

    [RelayCommand]
    private async Task SelectAlertAsync(SafetyAlert alert)
    {
        if (alert == null)
            return;

        SelectedAlert = alert;
        
        // Navigate to detail page (to be implemented)
        await Shell.Current.DisplayAlert(
            alert.Title,
            $"{alert.Description}\n\nLocation: {alert.Location.Address}, {alert.Location.City}\nDistance: {alert.DistanceFromUser:F1} km\nSeverity: {alert.Severity}\nTime: {alert.Timestamp:g}",
            "OK");
        
        SelectedAlert = null;
    }

    // Calculate distance between two points using Haversine formula
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Earth's radius in kilometers
        
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        
        return R * c;
    }

    private double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
