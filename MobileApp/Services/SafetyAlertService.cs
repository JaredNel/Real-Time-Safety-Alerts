using System.Net.Http.Json;
using SafetyAlertsApp.Models;

namespace SafetyAlertsApp.Services;

public interface ISafetyAlertService
{
    Task<List<SafetyAlert>> GetAlertsAsync();
    Task<SafetyAlert?> GetAlertByIdAsync(int id);
    Task<List<SafetyAlert>> GetAlertsByLocationAsync(double latitude, double longitude, double radiusKm);
}

public class SafetyAlertService : ISafetyAlertService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public SafetyAlertService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // This can be configured via app settings or environment variables
        _baseUrl = "https://api.safetyalerts.com/api/v1";
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<List<SafetyAlert>> GetAlertsAsync()
    {
        try
        {
            var alerts = await _httpClient.GetFromJsonAsync<List<SafetyAlert>>("alerts");
            return alerts ?? new List<SafetyAlert>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching alerts: {ex.Message}");
            // Return mock data for development/testing
            return GetMockAlerts();
        }
    }

    public async Task<SafetyAlert?> GetAlertByIdAsync(int id)
    {
        try
        {
            var alert = await _httpClient.GetFromJsonAsync<SafetyAlert>($"alerts/{id}");
            return alert;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching alert {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<List<SafetyAlert>> GetAlertsByLocationAsync(double latitude, double longitude, double radiusKm)
    {
        try
        {
            var alerts = await _httpClient.GetFromJsonAsync<List<SafetyAlert>>(
                $"alerts/location?lat={latitude}&lon={longitude}&radius={radiusKm}");
            return alerts ?? new List<SafetyAlert>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching alerts by location: {ex.Message}");
            // Return mock data for development/testing
            return GetMockAlerts();
        }
    }

    // Mock data for development and testing purposes
    private List<SafetyAlert> GetMockAlerts()
    {
        return new List<SafetyAlert>
        {
            new SafetyAlert
            {
                Id = 1,
                Title = "Severe Weather Alert",
                Description = "Heavy rain and thunderstorms expected in the area. Stay indoors and avoid travel if possible.",
                Severity = AlertSeverity.High,
                Type = AlertType.Weather,
                Location = new Models.Location
                {
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                    Address = "123 Main St",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001"
                },
                Timestamp = DateTime.Now.AddHours(-1),
                IsActive = true,
                DistanceFromUser = 2.5
            },
            new SafetyAlert
            {
                Id = 2,
                Title = "Traffic Accident",
                Description = "Major traffic accident on Highway 101. Expect delays and consider alternate routes.",
                Severity = AlertSeverity.Medium,
                Type = AlertType.Traffic,
                Location = new Models.Location
                {
                    Latitude = 40.7580,
                    Longitude = -73.9855,
                    Address = "Highway 101",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10019"
                },
                Timestamp = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                DistanceFromUser = 5.2
            },
            new SafetyAlert
            {
                Id = 3,
                Title = "Fire Incident",
                Description = "Building fire reported. Emergency services on scene. Avoid the area.",
                Severity = AlertSeverity.Critical,
                Type = AlertType.Fire,
                Location = new Models.Location
                {
                    Latitude = 40.7589,
                    Longitude = -73.9851,
                    Address = "456 Park Ave",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10022"
                },
                Timestamp = DateTime.Now.AddMinutes(-15),
                IsActive = true,
                DistanceFromUser = 1.8
            },
            new SafetyAlert
            {
                Id = 4,
                Title = "Air Quality Warning",
                Description = "Poor air quality detected. Sensitive individuals should limit outdoor activities.",
                Severity = AlertSeverity.Low,
                Type = AlertType.Environmental,
                Location = new Models.Location
                {
                    Latitude = 40.7489,
                    Longitude = -73.9680,
                    Address = "Downtown Area",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10017"
                },
                Timestamp = DateTime.Now.AddHours(-2),
                IsActive = true,
                DistanceFromUser = 3.7
            }
        };
    }
}
