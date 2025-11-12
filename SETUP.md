# ClearVolume Windows Setup Guide

This guide walks you through setting up and building the ClearVolume Windows application.

## Prerequisites

### Required Software

1. **.NET 6.0 SDK or later**
   - Download from: https://dotnet.microsoft.com/download/dotnet/6.0
   - Verify installation: `dotnet --version`

2. **Visual Studio 2022** (Recommended)
   - Download from: https://visualstudio.microsoft.com/
   - Install with ".NET desktop development" workload
   - Community edition is free and sufficient

3. **Windows 10 or later**
   - Windows 10 version 1809 or later
   - Windows 11 recommended

### Optional Software

- **Git**: For cloning repository
- **Camera**: Webcam or external camera connected to computer

## Installation Steps

### Step 1: Install .NET SDK

1. Download .NET 6.0 SDK from Microsoft website
2. Run installer and follow prompts
3. Verify installation:
   ```bash
   dotnet --version
   ```
   Should output: `6.0.x` or higher

### Step 2: Install Visual Studio 2022

1. Download Visual Studio 2022 Community (free)
2. Run installer
3. Select ".NET desktop development" workload
4. Install Visual Studio
5. Launch Visual Studio

### Step 3: Open Project

1. **Option A: Open Solution File**
   - Open Visual Studio
   - File → Open → Project/Solution
   - Navigate to `ClearVolumeWindows.sln`
   - Click Open

2. **Option B: Clone Repository**
   ```bash
   git clone <repository-url>
   cd ClearVolumeWindows
   ```

### Step 4: Restore NuGet Packages

1. In Visual Studio:
   - Right-click solution
   - Select "Restore NuGet Packages"
   - Wait for packages to download

2. Or using command line:
   ```bash
   dotnet restore
   ```

### Step 5: Build Project

1. **In Visual Studio**:
   - Press `F6` or Build → Build Solution
   - Wait for build to complete

2. **Using Command Line**:
   ```bash
   dotnet build
   ```

### Step 6: Run Application

1. **In Visual Studio**:
   - Press `F5` or Debug → Start Debugging
   - Application will launch

2. **Using Command Line**:
   ```bash
   dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj
   ```

## Configuration

### Camera Settings

1. **Grant Camera Permission**:
   - Windows Settings → Privacy → Camera
   - Enable "Allow apps to access your camera"
   - Ensure ClearVolume has access

2. **Select Camera** (if multiple cameras):
   - Currently uses default camera (index 0)
   - To change, modify `CameraManager.cs`:
     ```csharp
     _videoCapture = new VideoCapture(0); // Change 0 to camera index
     ```

### Application Settings

- Settings are accessible via Settings button in application
- Currently supports:
  - Dark mode (always enabled)
  - Units: Milliliters only

## Troubleshooting

### Build Errors

#### Error: "Package restore failed"

**Solution**:
1. Check internet connection
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Clear NuGet cache:
   ```bash
   dotnet nuget locals all --clear
   ```

#### Error: "OpenCV not found"

**Solution**:
1. OpenCV runtime is included via NuGet package
2. Ensure `OpenCvSharp4.runtime.win` package is installed
3. Restore NuGet packages
4. Rebuild solution

#### Error: ".NET SDK not found"

**Solution**:
1. Install .NET 6.0 SDK
2. Verify installation: `dotnet --version`
3. Restart Visual Studio
4. Check project target framework matches installed SDK

### Runtime Errors

#### Error: "Camera access denied"

**Solution**:
1. Go to Windows Settings → Privacy → Camera
2. Enable "Allow apps to access your camera"
3. Grant permission to ClearVolume
4. Restart application

#### Error: "Camera not found"

**Solution**:
1. Verify camera is connected
2. Test camera in other applications
3. Check device manager for camera
4. Restart computer if needed
5. Modify camera index in `CameraManager.cs` if multiple cameras

#### Error: "Application crashes on startup"

**Solution**:
1. Check error message in console
2. Verify all dependencies are installed
3. Check .NET runtime version
4. Rebuild solution
5. Check Windows Event Viewer for details

### Performance Issues

#### Low Frame Rate

**Solution**:
1. Close other applications using camera
2. Reduce camera resolution in `CameraManager.cs`
3. Increase processing interval in `MainWindow.xaml.cs`
4. Check CPU usage
5. Update graphics drivers

#### High CPU Usage

**Solution**:
1. Increase processing interval (process fewer frames)
2. Reduce image resolution
3. Optimize OpenCV operations
4. Check for background processes

## Development Setup

### Adding New Features

1. **Create New Class**:
   - Right-click project → Add → Class
   - Name your class
   - Implement functionality

2. **Add UI Component**:
   - Edit `MainWindow.xaml` for UI
   - Edit `MainWindow.xaml.cs` for logic

3. **Add Settings**:
   - Edit `SettingsWindow.xaml` for UI
   - Edit `SettingsWindow.xaml.cs` for logic

### Debugging

1. **Set Breakpoints**:
   - Click left margin in code editor
   - Red dot appears at breakpoint

2. **Start Debugging**:
   - Press `F5` to start debugging
   - Application pauses at breakpoints
   - Use debug toolbar to step through code

3. **View Variables**:
   - Hover over variables to see values
   - Use Watch window for expressions
   - Use Locals window for local variables

### Testing

1. **Test on Different Cameras**:
   - Test with built-in webcam
   - Test with external USB camera
   - Test with different resolutions

2. **Test with Different Bottles**:
   - Test with various bottle types
   - Test with different sizes
   - Test with different lighting

3. **Test Performance**:
   - Monitor frame rate
   - Monitor CPU usage
   - Monitor memory usage

## Building for Distribution

### Release Build

1. **In Visual Studio**:
   - Build → Configuration Manager
   - Set configuration to "Release"
   - Build solution (F6)

2. **Using Command Line**:
   ```bash
   dotnet build -c Release
   ```

### Create Installer (Optional)

1. **Using WiX Toolset**:
   - Install WiX Toolset
   - Create installer project
   - Add application files
   - Build installer

2. **Using Inno Setup**:
   - Download Inno Setup
   - Create installer script
   - Build installer

## Next Steps

1. **Test Application**: Test with various bottles and lighting
2. **Improve Detection**: Enhance bottle and fluid level detection
3. **Add ML Model**: Train and integrate ML model for better accuracy
4. **Optimize Performance**: Improve frame rate and CPU usage
5. **Polish UI**: Improve user interface and experience

## Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [WPF Documentation](https://docs.microsoft.com/dotnet/desktop/wpf/)
- [OpenCV Documentation](https://docs.opencv.org/)
- [Visual Studio Documentation](https://docs.microsoft.com/visualstudio/)

## Support

For issues or questions:
1. Check this setup guide
2. Review README.md
3. Check error messages
4. Verify all requirements are met
5. Open an issue on GitHub (if applicable)

---

**Last Updated**: [Current Date]
**Version**: 1.0.0

