# Quick Start Guide

Get the Real-Time Safety Alerts mobile app running on your machine in 5 minutes!

## Prerequisites

Before you begin, ensure you have:
- ✅ [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- ✅ A code editor ([Visual Studio 2022](https://visualstudio.microsoft.com/), [VS Code](https://code.visualstudio.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/))
- ✅ For Android: [Android SDK](https://developer.android.com/studio)
- ✅ For iOS: Mac with [Xcode](https://developer.apple.com/xcode/)

## Step 1: Clone the Repository

```bash
git clone https://github.com/JaredNel/Real-Time-Safety-Alerts.git
cd Real-Time-Safety-Alerts/MobileApp
```

## Step 2: Install .NET MAUI Workload

```bash
# For Android development
dotnet workload install maui-android

# For iOS development (Mac only)
dotnet workload install maui-ios
```

This may take a few minutes to download and install all required components.

## Step 3: Restore Dependencies

```bash
dotnet restore
```

## Step 4: Build the Application

```bash
# For Android
dotnet build -f net9.0-android

# For iOS (Mac only)
dotnet build -f net9.0-ios
```

## Step 5: Run the Application

### Option A: Using Command Line

```bash
# Run on Android emulator
dotnet run -f net9.0-android

# Run on iOS simulator (Mac only)
dotnet run -f net9.0-ios
```

### Option B: Using Visual Studio

1. Open `SafetyAlertsApp.csproj` in Visual Studio
2. Select your target platform (Android/iOS)
3. Choose an emulator/simulator or physical device
4. Click the Run button (▶️) or press F5

### Option C: Using VS Code

1. Open the `MobileApp` folder in VS Code
2. Install the [.NET MAUI extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-maui)
3. Press F5 to run or use the Run and Debug panel

## Step 6: Explore the App

Once the app is running:
1. **Grant Location Permission** when prompted (optional but recommended)
2. **View Safety Alerts** on the main screen
3. **Tap an alert** to see detailed information
4. **Pull down** to refresh the alert list
5. **Navigate to About** to learn about features

## Troubleshooting

### Build Errors

**Problem:** "Workload not found" error  
**Solution:** Run `dotnet workload install maui-android`

**Problem:** Build fails with Android SDK errors  
**Solution:** Ensure Android SDK is installed via Android Studio

**Problem:** iOS build fails on Windows  
**Solution:** iOS builds require macOS with Xcode

### Runtime Issues

**Problem:** App crashes on launch  
**Solution:** 
- Check emulator/device is compatible (Android 5.0+, iOS 15.0+)
- Try cleaning and rebuilding: `dotnet clean && dotnet build`

**Problem:** No alerts showing  
**Solution:** This is expected without a backend API. The app will show mock data for testing.

**Problem:** Location permission denied  
**Solution:** The app will work without location but won't show distances. You can grant permission in device settings.

## Next Steps

### For Developers

1. **Read the Architecture**: Check [README.md](README.md) for architecture details
2. **Review the Code**: Explore the Models, Services, ViewModels, and Views
3. **Make Changes**: Try modifying the UI or adding new features
4. **Run Tests**: See [TESTING.md](TESTING.md) for testing guidelines

### For Testers

1. **Follow Test Plan**: See [TESTING.md](TESTING.md) for comprehensive testing scenarios
2. **Report Issues**: Use GitHub Issues with the bug report template
3. **Test on Multiple Devices**: Try different Android/iOS versions

### For DevOps

1. **Setup CI/CD**: Follow [DEPLOYMENT.md](DEPLOYMENT.md) for pipeline configuration
2. **Configure Signing**: Set up keystores and certificates
3. **Deploy to Stores**: Prepare for Google Play and App Store submission

## Common Commands

```bash
# Clean build artifacts
dotnet clean

# Restore packages
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios

# Build for Release
dotnet build -f net9.0-android -c Release

# Publish for distribution
dotnet publish -f net9.0-android -c Release

# List installed workloads
dotnet workload list

# Update workloads
dotnet workload update
```

## Project Structure Quick Reference

```
MobileApp/
├── Models/              → Data structures
├── Services/           → API and platform services
├── ViewModels/         → MVVM view models
├── Views/              → UI pages (XAML)
├── Converters/         → Value converters
├── Platforms/          → Platform-specific code
└── Resources/          → Images, fonts, styles
```

## Key Files

- `MauiProgram.cs` - App initialization and dependency injection
- `App.xaml` - Global resources and styles
- `AppShell.xaml` - Navigation structure
- `SafetyAlertsPage.xaml` - Main alerts UI
- `SafetyAlertService.cs` - API integration (with mock data)

## Getting Help

- 📖 **Documentation**: Check the `/MobileApp/*.md` files
- 🐛 **Issues**: [GitHub Issues](https://github.com/JaredNel/Real-Time-Safety-Alerts/issues)
- 💬 **Discussions**: [GitHub Discussions](https://github.com/JaredNel/Real-Time-Safety-Alerts/discussions)
- 📚 **MAUI Docs**: [Microsoft Learn](https://learn.microsoft.com/dotnet/maui/)

## Quick Development Tips

1. **Hot Reload**: MAUI supports hot reload for XAML changes
2. **Debugging**: Use breakpoints and debug console for troubleshooting
3. **Logging**: Check console output for service logs
4. **Mock Data**: Located in `SafetyAlertService.GetMockAlerts()`
5. **Styles**: Global styles are in `Resources/Styles/`

## Development Workflow

```bash
# 1. Make changes to code
# 2. Build to check for errors
dotnet build -f net9.0-android

# 3. Run on emulator/device
dotnet run -f net9.0-android

# 4. Test your changes
# 5. Commit when ready
git add .
git commit -m "Your changes"
git push
```

## Ready to Go! 🚀

You should now have the app running on your device/emulator. Start exploring the code and building amazing features!

For detailed documentation, see:
- [README.md](README.md) - Complete overview
- [API_INTEGRATION.md](API_INTEGRATION.md) - Backend integration
- [DEPLOYMENT.md](DEPLOYMENT.md) - Production deployment
- [TESTING.md](TESTING.md) - Testing guide
- [PROJECT_SUMMARY.md](PROJECT_SUMMARY.md) - Project overview
