# Testing Guide for Real-Time Safety Alerts Mobile App

This document outlines the testing strategy and procedures for ensuring the mobile application works correctly across all supported platforms.

## Test Environment Setup

### Required Test Devices

**Android:**
- Physical devices: Android 5.0+ (API 21+)
- Emulators: Android Studio AVD with Google Play Services

**iOS:**
- Physical devices: iOS 15.0+
- Simulators: Xcode iOS Simulator

### Test Data Setup

The app includes mock data in `SafetyAlertService.cs` for testing without a backend. This data includes:
- 4 sample alerts with different severities
- Various alert types (Weather, Traffic, Fire, Environmental)
- Different locations in New York area

## Test Categories

### 1. Unit Tests

#### Models Tests
```csharp
// Test SafetyAlert model
[Test]
public void SafetyAlert_PropertiesSet_Correctly()
{
    var alert = new SafetyAlert
    {
        Id = 1,
        Title = "Test Alert",
        Severity = AlertSeverity.High
    };
    
    Assert.AreEqual(1, alert.Id);
    Assert.AreEqual("Test Alert", alert.Title);
    Assert.AreEqual(AlertSeverity.High, alert.Severity);
}
```

#### Services Tests
```csharp
// Test SafetyAlertService
[Test]
public async Task GetAlertsAsync_ReturnsMockData()
{
    var httpClient = new HttpClient();
    var service = new SafetyAlertService(httpClient);
    
    var alerts = await service.GetAlertsAsync();
    
    Assert.IsNotNull(alerts);
    Assert.IsTrue(alerts.Count > 0);
}
```

#### ViewModels Tests
```csharp
// Test SafetyAlertsViewModel
[Test]
public async Task LoadAlertsCommand_LoadsAlerts()
{
    var service = new Mock<ISafetyAlertService>();
    var locationService = new Mock<ILocationService>();
    var viewModel = new SafetyAlertsViewModel(service.Object, locationService.Object);
    
    await viewModel.LoadAlertsCommand.ExecuteAsync(null);
    
    Assert.IsFalse(viewModel.IsLoading);
    Assert.IsTrue(viewModel.Alerts.Count > 0);
}
```

### 2. Integration Tests

#### API Integration
- Test network connectivity
- Test API endpoint responses
- Test error handling for failed requests
- Test timeout scenarios

#### Location Services
- Test permission request flow
- Test location retrieval
- Test distance calculations
- Test location updates

### 3. UI Tests

#### Navigation Tests
```csharp
[Test]
public void Shell_Navigation_Works()
{
    // Navigate to SafetyAlertsPage
    Shell.Current.GoToAsync("//SafetyAlertsPage");
    
    // Verify we're on the correct page
    Assert.IsInstanceOf<SafetyAlertsPage>(Shell.Current.CurrentPage);
}
```

#### Data Binding Tests
- Verify alerts display in list
- Verify severity badges show correct colors
- Verify distance calculations display
- Verify timestamps format correctly

## Manual Testing Checklist

### Installation & First Launch

- [ ] App installs successfully
- [ ] App icon displays correctly
- [ ] Splash screen shows properly
- [ ] Initial launch completes without crash
- [ ] Location permission prompt appears
- [ ] App handles permission denial gracefully

### Main Functionality

#### Alert List Display
- [ ] Alerts load and display correctly
- [ ] Severity badges show correct colors:
  - Critical: Red
  - High: Orange
  - Medium: Gold
  - Low: Green
- [ ] Distance from user location calculates correctly
- [ ] Alert titles and descriptions display fully
- [ ] Location information shows correctly
- [ ] Timestamps format properly
- [ ] Empty state shows when no alerts available

#### User Interactions
- [ ] Tap on alert shows detail dialog
- [ ] Pull-to-refresh reloads data
- [ ] Loading indicator appears during refresh
- [ ] Status message updates appropriately
- [ ] Multiple rapid refreshes don't crash app

#### Navigation
- [ ] Tab navigation works (Safety Alerts ↔ About)
- [ ] Back button functions correctly
- [ ] Deep linking works (if implemented)

### Location Services

- [ ] App requests location permission on first use
- [ ] App handles permission granted correctly
- [ ] App handles permission denied gracefully
- [ ] Location retrieves successfully when granted
- [ ] Distance calculations are accurate
- [ ] Alerts sort by distance from user

### Network Scenarios

#### Online Mode
- [ ] Alerts load from API (if backend available)
- [ ] API errors display user-friendly messages
- [ ] Network timeouts handled gracefully

#### Offline Mode
- [ ] Mock data loads when offline
- [ ] Appropriate message shows for offline state
- [ ] App doesn't crash without internet

### Performance

- [ ] App launches in < 3 seconds
- [ ] Alert list scrolls smoothly
- [ ] No memory leaks during extended use
- [ ] App responds to user input immediately
- [ ] No UI freezes or ANR (Application Not Responding)

### Platform-Specific Tests

#### Android Specific
- [ ] App works on various Android versions (5.0+)
- [ ] Works on different screen sizes/densities
- [ ] Back button behavior is correct
- [ ] App appears in recent apps correctly
- [ ] Notifications work (if implemented)

#### iOS Specific
- [ ] App works on various iOS versions (15.0+)
- [ ] Works on different iPhone models
- [ ] Works on iPad (if supported)
- [ ] Status bar displays correctly
- [ ] Safe area layouts work properly

## Test Scenarios

### Scenario 1: First-Time User Experience
1. Install the app
2. Launch the app
3. Grant location permission when prompted
4. Verify alerts load and display
5. Tap on an alert to view details
6. Navigate to About page
7. Return to Safety Alerts page
8. Pull to refresh

**Expected Result:** Smooth onboarding experience with no crashes

### Scenario 2: Location Permission Denied
1. Launch the app
2. Deny location permission
3. Verify alerts still display (without distance)
4. Navigate through app
5. Go to Settings and grant permission
6. Return to app
7. Verify distance now calculates

**Expected Result:** App functions without location, improves with permission

### Scenario 3: Network Loss During Use
1. Launch app with internet connection
2. Wait for alerts to load
3. Disable network connection
4. Try to refresh alerts
5. Re-enable network connection
6. Refresh again

**Expected Result:** Graceful handling of network changes

### Scenario 4: Multiple Alerts Viewing
1. Launch app
2. View list of alerts
3. Tap each alert one by one
4. Verify all details display correctly
5. Check sorting by distance
6. Verify severity color coding

**Expected Result:** All alerts display correctly with proper visual indicators

## Performance Benchmarks

### Target Metrics
- **App Launch Time:** < 3 seconds
- **Alert List Load Time:** < 2 seconds
- **Memory Usage:** < 100 MB
- **API Response Time:** < 1 second
- **Location Retrieval:** < 5 seconds
- **Frame Rate:** 60 FPS during scrolling

### Profiling Tools
- **Android:** Android Profiler in Android Studio
- **iOS:** Instruments in Xcode
- **.NET:** dotnet-trace, dotnet-counters

## Accessibility Testing

### VoiceOver/TalkBack
- [ ] All UI elements have accessibility labels
- [ ] Navigation works with screen reader
- [ ] Alerts are announced properly
- [ ] Buttons have clear descriptions

### Visual Accessibility
- [ ] Text contrast meets WCAG standards
- [ ] Font sizes are readable
- [ ] Color coding also uses shapes/text
- [ ] Works with increased text sizes

## Security Testing

- [ ] Location data not leaked
- [ ] API keys not exposed in code
- [ ] HTTPS used for all API calls
- [ ] No sensitive data in logs
- [ ] Permissions requested appropriately

## Regression Testing

After any code changes, retest:
- Core functionality (alert loading)
- Navigation between pages
- Location services
- Pull-to-refresh
- Error handling
- UI rendering

## Bug Reporting Template

When reporting bugs, include:
```
**Environment:**
- Device/Emulator:
- OS Version:
- App Version:

**Steps to Reproduce:**
1. 
2. 
3. 

**Expected Behavior:**


**Actual Behavior:**


**Screenshots/Logs:**


**Additional Context:**

```

## Test Automation

### Recommended Frameworks
- **UI Testing:** Appium
- **Unit Testing:** NUnit, xUnit
- **Mocking:** Moq, NSubstitute

### CI/CD Integration
```yaml
# Example GitHub Actions workflow
- name: Run Tests
  run: dotnet test MobileApp.Tests/
  
- name: Run UI Tests
  run: appium run tests/ui/
```

## Test Coverage Goals

- **Unit Tests:** > 80% code coverage
- **Integration Tests:** All critical paths
- **UI Tests:** Major user workflows
- **Manual Tests:** Full regression suite before release

## Known Issues & Limitations

Document any known issues:
- iOS build requires macOS
- Mock data only in offline mode
- Distance calculation requires GPS permission

## Test Sign-Off

Before release, ensure:
- [ ] All critical bugs fixed
- [ ] Test coverage meets goals
- [ ] Performance benchmarks met
- [ ] Accessibility standards met
- [ ] Security audit passed
- [ ] All platforms tested
- [ ] Regression tests passed

## Support

For testing assistance:
- Review test code in repository
- Check CI/CD pipeline results
- Consult with QA team
- Review crash reports in app store dashboards
