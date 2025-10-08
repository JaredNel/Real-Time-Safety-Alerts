# Real-Time-Safety-Alerts

This repository contains the Issues Log and project management setup for Real-Time Safety Alerts (WBS integration).

## Project Overview

Real-Time Safety Alerts is a comprehensive system for delivering location-based safety notifications to users through a cross-platform mobile application.

## Components

### Mobile Application (`/MobileApp`)

A cross-platform mobile application built with .NET MAUI that provides:
- Real-time safety alerts based on user location
- Multi-platform support (iOS and Android)
- Location-based alert filtering
- Severity-based alert categorization
- Integration with backend API for live data

**Technologies Used:**
- .NET 9.0
- .NET MAUI (Multi-platform App UI)
- C# with MVVM pattern
- REST API integration

For detailed mobile app documentation, see [MobileApp/README.md](MobileApp/README.md).

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022 or VS Code
- Android SDK (for Android development)
- Xcode (for iOS development, Mac only)

### Quick Start

1. Clone the repository:
   ```bash
   git clone https://github.com/JaredNel/Real-Time-Safety-Alerts.git
   cd Real-Time-Safety-Alerts
   ```

2. Install .NET MAUI workload:
   ```bash
   dotnet workload install maui-android
   ```

3. Build and run the mobile app:
   ```bash
   cd MobileApp
   dotnet restore
   dotnet build -f net9.0-android
   dotnet run -f net9.0-android
   ```

## Repository Structure

```
Real-Time-Safety-Alerts/
├── MobileApp/              # Cross-platform mobile application
│   ├── Models/            # Data models
│   ├── Services/          # API and platform services
│   ├── ViewModels/        # MVVM view models
│   ├── Views/             # UI pages and components
│   └── README.md          # Mobile app documentation
└── README.md              # This file
```

## Development Status

- ✅ Mobile app structure created
- ✅ Core functionality implemented
- ✅ Location services integrated
- ✅ API service layer ready
- ✅ Cross-platform UI developed
- ⏳ Backend API integration (pending)
- ⏳ Push notifications (planned)

## Contributing

Contributions are welcome! Please read the contributing guidelines before submitting pull requests.

## License

[Specify your license here]

## Contact

For questions or support, please open an issue in this repository.
