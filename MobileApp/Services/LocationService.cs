namespace SafetyAlertsApp.Services;

public interface ILocationService
{
    Task<Microsoft.Maui.Devices.Sensors.Location?> GetCurrentLocationAsync();
    Task<bool> CheckAndRequestLocationPermissionAsync();
}

public class LocationService : ILocationService
{
    public async Task<Microsoft.Maui.Devices.Sensors.Location?> GetCurrentLocationAsync()
    {
        try
        {
            var hasPermission = await CheckAndRequestLocationPermissionAsync();
            if (!hasPermission)
            {
                Console.WriteLine("Location permission not granted");
                return null;
            }

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);
            
            return location;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting location: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CheckAndRequestLocationPermissionAsync()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            return status == PermissionStatus.Granted;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking location permission: {ex.Message}");
            return false;
        }
    }
}
