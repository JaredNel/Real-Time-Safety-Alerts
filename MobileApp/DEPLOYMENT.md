# Deployment Guide for Real-Time Safety Alerts Mobile App

This guide covers the deployment process for publishing the mobile application to app stores and distributing to end users.

## Prerequisites

### General Requirements
- Valid developer accounts (Apple Developer, Google Play Console)
- Code signing certificates
- App store assets (icons, screenshots, descriptions)
- Privacy policy and terms of service URLs
- Completed app testing

### iOS Requirements
- Apple Developer Program membership ($99/year)
- Mac with Xcode installed
- iOS Distribution Certificate
- App Store provisioning profile
- iTunes Connect access

### Android Requirements
- Google Play Developer account ($25 one-time fee)
- Keystore for signing APK/AAB files
- Google Play Console access

## Build for Production

### Android

#### 1. Create Keystore (First Time Only)

```bash
keytool -genkey -v -keystore safetyalerts.keystore -alias safetyalerts -keyalg RSA -keysize 2048 -validity 10000
```

**Store this keystore securely!** You'll need it for all future updates.

#### 2. Update Project Configuration

Edit `SafetyAlertsApp.csproj` to add signing configuration:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <AndroidKeyStore>true</AndroidKeyStore>
  <AndroidSigningKeyStore>path/to/safetyalerts.keystore</AndroidSigningKeyStore>
  <AndroidSigningKeyAlias>safetyalerts</AndroidSigningKeyAlias>
  <AndroidSigningKeyPass>your-keystore-password</AndroidSigningKeyPass>
  <AndroidSigningStorePass>your-store-password</AndroidSigningStorePass>
</PropertyGroup>
```

**Security Note:** Never commit passwords to source control. Use environment variables or secure vaults.

#### 3. Build Release APK/AAB

For APK (Direct distribution):
```bash
dotnet publish -f net9.0-android -c Release
```

For AAB (Google Play Store):
```bash
dotnet publish -f net9.0-android -c Release -p:AndroidPackageFormat=aab
```

Output location: `bin/Release/net9.0-android/publish/`

#### 4. Test Release Build

Before submission, test the release build:
```bash
adb install path/to/com.safetyalerts.realtimeapp-Signed.apk
```

### iOS

#### 1. Configure Signing

In your `SafetyAlertsApp.csproj`, add:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <CodesignKey>iPhone Distribution: Your Company Name</CodesignKey>
  <CodesignProvision>Your Distribution Provisioning Profile</CodesignProvision>
  <ArchiveOnBuild>true</ArchiveOnBuild>
</PropertyGroup>
```

#### 2. Build for App Store

```bash
dotnet publish -f net9.0-ios -c Release
```

#### 3. Archive and Upload

1. Open the `.xcarchive` in Xcode
2. Click "Distribute App"
3. Select "App Store Connect"
4. Follow the wizard to upload

## Google Play Store Deployment

### 1. Prepare Store Listing

Required assets:
- App icon (512x512 PNG)
- Feature graphic (1024x500 PNG)
- Screenshots (2-8 images per device type)
- Short description (80 characters max)
- Full description (4000 characters max)
- Privacy policy URL
- Content rating questionnaire

### 2. Create Release

1. Log into [Google Play Console](https://play.google.com/console)
2. Select your app or create new application
3. Navigate to "Release" → "Production"
4. Click "Create new release"
5. Upload your AAB file
6. Add release notes
7. Review and roll out

### 3. Release Tracks

**Internal Testing:**
- Fast review process
- Limited to 100 testers
- Instant updates

**Closed Beta:**
- Up to 2000 testers
- Useful for pre-launch testing

**Open Beta:**
- Public testing
- Available to anyone via opt-in link

**Production:**
- Live in Google Play Store
- All users can download

### 4. Staged Rollout

Gradually release to users:
```
1. Start with 5% of users
2. Monitor for crashes/issues
3. Increase to 20% after 24 hours
4. Increase to 50% after 48 hours
5. Full rollout (100%) after 72 hours
```

## Apple App Store Deployment

### 1. Prepare Store Listing

Required assets:
- App icon (1024x1024 PNG)
- Screenshots for each device size
- App preview videos (optional)
- Description
- Keywords
- Support URL
- Marketing URL (optional)
- Privacy policy URL

### 2. App Store Connect Setup

1. Log into [App Store Connect](https://appstoreconnect.apple.com)
2. Create new app
3. Fill in app information:
   - Name
   - Bundle ID: `com.safetyalerts.realtimeapp`
   - SKU
   - Primary language

### 3. Submit for Review

1. Upload build via Xcode or Transporter
2. Complete all required information
3. Submit for review
4. Wait for approval (typically 24-48 hours)

### 4. Release Options

**Automatic Release:**
- App goes live immediately after approval

**Manual Release:**
- Choose when to release after approval

**Phased Release:**
- Gradually release to users over 7 days

## Configuration Updates

### 1. Production API Endpoint

Update `Services/SafetyAlertService.cs`:

```csharp
// Development
_baseUrl = "https://dev-api.safetyalerts.com/api/v1";

// Production
_baseUrl = "https://api.safetyalerts.com/api/v1";
```

### 2. Environment-Specific Configuration

Use compilation symbols:

```csharp
#if DEBUG
    _baseUrl = "https://dev-api.safetyalerts.com/api/v1";
#else
    _baseUrl = "https://api.safetyalerts.com/api/v1";
#endif
```

### 3. Version Numbering

Update in `SafetyAlertsApp.csproj`:

```xml
<ApplicationDisplayVersion>1.0</ApplicationDisplayVersion>
<ApplicationVersion>1</ApplicationVersion>
```

**Versioning Strategy:**
- `ApplicationDisplayVersion`: User-facing version (1.0, 1.1, 2.0)
- `ApplicationVersion`: Build number (increments with each release)

## Testing Checklist

Before releasing:

- [ ] All features work correctly
- [ ] No crashes or major bugs
- [ ] Performance is acceptable
- [ ] Location permissions work
- [ ] API integration successful
- [ ] UI looks good on various screen sizes
- [ ] Network error handling works
- [ ] Offline mode functions properly
- [ ] App icons and splash screens display correctly
- [ ] Terms of service and privacy policy accessible
- [ ] Analytics and crash reporting configured

## Post-Release

### Monitor Performance

1. **Crash Reports:**
   - Google Play Console → Android vitals
   - App Store Connect → Crashes

2. **User Reviews:**
   - Respond to user feedback
   - Address common issues in updates

3. **Analytics:**
   - Monitor user engagement
   - Track feature usage
   - Identify problem areas

### Release Updates

For updates:
1. Increment version numbers
2. Build new release
3. Test thoroughly
4. Submit to stores
5. Include release notes describing changes

## Continuous Integration/Deployment

### GitHub Actions (Example)

Create `.github/workflows/android-release.yml`:

```yaml
name: Android Release

on:
  push:
    tags:
      - 'v*'

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      
      - name: Install MAUI
        run: dotnet workload install maui-android
      
      - name: Build
        run: |
          cd MobileApp
          dotnet publish -f net9.0-android -c Release
      
      - name: Upload Artifact
        uses: actions/upload-artifact@v3
        with:
          name: android-release
          path: MobileApp/bin/Release/net9.0-android/publish/
```

## Security Best Practices

1. **Never commit sensitive data:**
   - API keys
   - Signing passwords
   - Keystore files

2. **Use secure storage:**
   - Azure Key Vault
   - GitHub Secrets
   - Environment variables

3. **Enable ProGuard/R8:**
   - Obfuscate code
   - Reduce APK size

4. **Implement certificate pinning:**
   - Prevent man-in-the-middle attacks

5. **Regular security audits:**
   - Update dependencies
   - Fix vulnerabilities

## Troubleshooting

### Android Build Issues

**Problem:** Build fails with signing errors
**Solution:** Verify keystore path and passwords are correct

**Problem:** AAB size too large
**Solution:** Enable ProGuard/R8, remove unused resources

### iOS Build Issues

**Problem:** Provisioning profile errors
**Solution:** Regenerate provisioning profiles in Apple Developer Portal

**Problem:** Archive not showing in Xcode
**Solution:** Ensure generic device is selected for build

## Support

For deployment assistance:
- Google Play Support: https://support.google.com/googleplay/android-developer
- Apple Developer Support: https://developer.apple.com/support/
- .NET MAUI Documentation: https://docs.microsoft.com/dotnet/maui/
