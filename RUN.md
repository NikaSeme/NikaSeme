# How to Run ClearVolume Windows App

## Quick Start (Command Line)

### Prerequisites Check

1. **Check if .NET 6.0 SDK is installed**:
   ```bash
   dotnet --version
   ```
   Should show: `6.0.x` or higher
   
   If not installed: Download from https://dotnet.microsoft.com/download/dotnet/6.0

2. **Navigate to project directory**:
   ```bash
   cd ClearVolumeWindows
   ```

### Step 1: Restore Dependencies

```bash
dotnet restore
```

This downloads all NuGet packages (OpenCV, ML.NET, etc.)

### Step 2: Build the Project

```bash
dotnet build
```

This compiles the application. Wait for "Build succeeded" message.

### Step 3: Run the Application

```bash
dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj
```

Or simply:
```bash
dotnet run
```

The app will launch and open the camera view!

## Visual Studio Method

### Step 1: Open Solution

1. Open **Visual Studio 2022** (Community edition is free)
2. File → Open → Project/Solution
3. Navigate to `ClearVolumeWindows` folder
4. Select `ClearVolumeWindows.sln`
5. Click Open

### Step 2: Restore NuGet Packages

1. Right-click on solution in Solution Explorer
2. Select **"Restore NuGet Packages"**
3. Wait for packages to download

### Step 3: Build

1. Press **F6** or
2. Build → Build Solution
3. Wait for "Build succeeded" message

### Step 4: Run

1. Press **F5** or
2. Debug → Start Debugging
3. The app will launch!

## First Run Checklist

1. ✅ **Camera Permission**: Windows may ask for camera permission - click "Allow"
2. ✅ **Camera Connected**: Make sure your webcam/camera is connected
3. ✅ **Point at Bottle**: Point camera at a clear bottle
4. ✅ **Good Lighting**: Ensure good lighting for best results
5. ✅ **Wait for Detection**: May take 1-2 seconds to detect bottle

## Troubleshooting

### Error: "dotnet command not found"

**Solution**: Install .NET 6.0 SDK
- Download: https://dotnet.microsoft.com/download/dotnet/6.0
- Install and restart terminal

### Error: "Camera access denied"

**Solution**: 
1. Windows Settings → Privacy → Camera
2. Enable "Allow apps to access your camera"
3. Restart app

### Error: "Camera not found"

**Solution**:
1. Check camera is connected
2. Test camera in other apps (e.g., Camera app)
3. Restart computer if needed
4. Check Device Manager for camera

### Error: "NuGet packages failed to restore"

**Solution**:
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Error: "Build failed"

**Solution**:
1. Check error messages in console
2. Ensure .NET 6.0 SDK is installed
3. Try: `dotnet clean` then `dotnet build`
4. Check Visual Studio Output window for details

### App runs but no camera view

**Solution**:
1. Check camera permission in Windows Settings
2. Verify camera is working in other apps
3. Check camera index in `CameraManager.cs` (default is 0)
4. Try different camera index if multiple cameras

### App runs but no volume estimate

**Solution**:
1. **Improve lighting**: Use bright, even lighting
2. **Move closer**: Position camera 30-50cm from bottle
3. **Clear view**: Ensure bottle is fully visible
4. **Wait**: Detection may take 1-2 seconds
5. **Use clear bottles**: Works best with transparent bottles

## Running from Built Executable

After building, you can run the executable directly:

1. **Navigate to build folder**:
   ```bash
   cd ClearVolumeWindows\ClearVolumeWindows\bin\Debug\net6.0-windows
   ```

2. **Run executable**:
   ```bash
   .\ClearVolume.exe
   ```

Or double-click `ClearVolume.exe` in File Explorer.

## Performance Tips

- **Close other camera apps**: Only one app can access camera at a time
- **Good lighting**: Improves detection accuracy
- **Steady camera**: Hold camera steady for better detection
- **Clear background**: Use contrasting background

## Expected Behavior

1. **App launches**: Window opens with black background
2. **Camera activates**: Camera preview appears (may take 1-2 seconds)
3. **Guidance message**: Shows "Point camera at clear bottle"
4. **Bottle detected**: Guidance hides, volume estimate appears
5. **Volume updates**: Updates in real-time as camera moves

## Next Steps

1. ✅ Test with different bottles
2. ✅ Test in different lighting conditions
3. ✅ Test at different distances
4. ✅ Adjust settings if needed
5. ✅ Report any issues

## Quick Command Reference

```bash
# Navigate to project
cd ClearVolumeWindows

# Restore packages
dotnet restore

# Build project
dotnet build

# Run application
dotnet run

# Clean build
dotnet clean
dotnet build

# Run in Release mode
dotnet build -c Release
dotnet run -c Release
```

## Need Help?

1. Check error messages in console
2. Review `SETUP.md` for detailed setup
3. Review `README.md` for documentation
4. Check Windows Event Viewer for errors
5. Verify all requirements are met

---

**Happy measuring! 🥤**

