# Real-Time Safety Alerts Mobile Application

A cross-platform mobile application for iOS and Android that provides real-time safety alerts based on user location, built with .NET MAUI.

## Features

- 📱 **Cross-Platform**: Works on both iOS and Android devices
- 🌍 **Location-Based Alerts**: Get safety alerts relevant to your current location
- ⚡ **Real-Time Updates**: Receive immediate notifications about safety events
- 📊 **Severity Levels**: Alerts categorized by severity (Critical, High, Medium, Low)
- 🎯 **Distance Calculation**: See how far alerts are from your current location
- 🔄 **Pull-to-Refresh**: Easy refresh mechanism to get the latest alerts
- 🎨 **Modern UI**: Clean, Material Design-inspired interface

## Technology Stack

- **.NET 9.0** - Latest .NET framework
- **.NET MAUI** - Cross-platform UI framework
- **C#** - Primary programming language
- **XAML** - UI markup language
- **CommunityToolkit.Mvvm** - MVVM pattern implementation
- **HttpClient** - REST API communication

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

### Project Structure

```
MobileApp/
├── Models/
│   └── SafetyAlert.cs          # Data models for alerts and locations
├── Services/
│   ├── SafetyAlertService.cs   # API service for fetching alerts
│   └── LocationService.cs       # Location/GPS service
├── ViewModels/
│   └── SafetyAlertsViewModel.cs # View logic and data binding
├── Views/
│   └── SafetyAlertsPage.xaml    # Main alerts UI
├── Converters/
│   └── SeverityToColorConverter.cs # UI value converters
├── Platforms/
│   ├── Android/                 # Android-specific code
│   └── iOS/                     # iOS-specific code (when available)
└── Resources/                   # Images, fonts, styles
```

## Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 (Windows) or Visual Studio for Mac / VS Code with appropriate extensions
- For Android development:
  - Android SDK (API 21 or higher)
  - Android Emulator or physical device
- For iOS development (Mac only):
  - Xcode
  - iOS Simulator or physical device

## Setup and Installation

### 1. Install .NET MAUI Workload

```bash
dotnet workload install maui-android
# For iOS (Mac only)
dotnet workload install maui-ios
```

### 2. Restore Dependencies

```bash
cd MobileApp
dotnet restore
```

### 3. Build the Application

For Android:
```bash
dotnet build -f net9.0-android
```

For iOS (Mac only):
```bash
dotnet build -f net9.0-ios
```

### 4. Run the Application

#### Android Emulator:
```bash
dotnet run -f net9.0-android
```

#### iOS Simulator (Mac only):
```bash
dotnet run -f net9.0-ios
```

#### Physical Device:
1. Enable developer mode on your device
2. Connect via USB
3. Run the appropriate build command

## Backend API Integration

The app is designed to integrate with a backend API. The base URL is configured in `Services/SafetyAlertService.cs`:

```csharp
_baseUrl = "https://api.safetyalerts.com/api/v1";
```

### API Endpoints Expected

- `GET /alerts` - Retrieve all active alerts
- `GET /alerts/{id}` - Get specific alert details
- `GET /alerts/location?lat={lat}&lon={lon}&radius={km}` - Get alerts by location

### Mock Data

For development and testing, the app includes mock data when the backend is unavailable. This can be found in the `GetMockAlerts()` method in `SafetyAlertService.cs`.

## Permissions

### Android
The following permissions are configured in `AndroidManifest.xml`:
- `ACCESS_COARSE_LOCATION` - Approximate location
- `ACCESS_FINE_LOCATION` - Precise location
- `ACCESS_NETWORK_STATE` - Network connectivity status
- `INTERNET` - Network communication

### iOS
Add the following to `Info.plist` when building for iOS:
```xml
<key>NSLocationWhenInUseUsageDescription</key>
<string>We need your location to show nearby safety alerts</string>
```

## Configuration

### App Settings
Update the following in `SafetyAlertsApp.csproj`:
- `ApplicationId` - Unique app identifier
- `ApplicationTitle` - Display name
- `ApplicationDisplayVersion` - Version shown to users
- `ApplicationVersion` - Build number

### Color Theme
Customize the app colors in `Resources/Styles/Colors.xaml`

## Testing

### Unit Testing
Create unit tests for ViewModels and Services:
```bash
dotnet test
```

### Manual Testing
1. Build and deploy to a device/emulator
2. Grant location permissions when prompted
3. Pull down to refresh alerts
4. Tap on an alert to view details
5. Verify distance calculations from current location

## Known Limitations

- iOS build requires macOS with Xcode installed
- Backend API integration requires a valid endpoint
- Push notifications are not yet implemented
- Offline mode is limited to cached data

## Future Enhancements

- [ ] Push notifications for critical alerts
- [ ] Offline support with local database
- [ ] Map view showing alert locations
- [ ] Filter alerts by type and severity
- [ ] User preferences and settings
- [ ] Share alerts with contacts
- [ ] Alert history and archive
- [ ] Dark mode support

## Troubleshooting

### Build Errors
- Ensure all workloads are installed: `dotnet workload list`
- Clean and rebuild: `dotnet clean && dotnet build`
- Check SDK version: `dotnet --version`

### Location Not Working
- Verify location permissions are granted
- Check device location services are enabled
- Test on a physical device (emulators may have limited GPS)

### API Connection Issues
- Verify network connectivity
- Check backend API is running and accessible
- Review API base URL configuration
- Enable debug logging to see HTTP requests

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly on both platforms
5. Submit a pull request

## License

This project is part of the Real-Time Safety Alerts system.

## Support

For issues and questions, please open an issue in the GitHub repository.
