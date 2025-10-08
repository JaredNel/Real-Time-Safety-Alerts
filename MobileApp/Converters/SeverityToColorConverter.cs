using System.Globalization;
using SafetyAlertsApp.Models;

namespace SafetyAlertsApp.Converters;

public class SeverityToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is AlertSeverity severity)
        {
            return severity switch
            {
                AlertSeverity.Critical => Colors.Red,
                AlertSeverity.High => Colors.Orange,
                AlertSeverity.Medium => Colors.Gold,
                AlertSeverity.Low => Colors.Green,
                _ => Colors.Gray
            };
        }
        return Colors.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
