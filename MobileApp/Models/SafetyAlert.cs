namespace SafetyAlertsApp.Models;

public class SafetyAlert
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AlertSeverity Severity { get; set; }
    public AlertType Type { get; set; }
    public Location Location { get; set; } = new();
    public DateTime Timestamp { get; set; }
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }
    public double? DistanceFromUser { get; set; }
}

public enum AlertSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum AlertType
{
    Weather,
    Crime,
    Traffic,
    Fire,
    Medical,
    Environmental,
    Other
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
