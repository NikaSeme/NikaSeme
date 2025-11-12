# How to Run ClearVolume Windows App

## ✅ Quick Start (3 Steps)

### Step 1: Open Terminal/Command Prompt
Navigate to the project directory:
```bash
cd "C:\Users\nikol\OneDrive\Desktop\CEU\python\training\ClearVolumeWindows"
```

### Step 2: Run the Application
```bash
dotnet run
```

That's it! The app will launch automatically.

---

## Alternative Methods

### Method 1: Using the Batch Script (Easiest)

**Windows:**
1. Double-click `run.bat` in the `ClearVolumeWindows` folder
2. The app will build and run automatically

### Method 2: Using Visual Studio

1. **Open Solution**:
   - Double-click `ClearVolumeWindows.sln`
   - Or open Visual Studio → File → Open → Project/Solution → select `ClearVolumeWindows.sln`

2. **Restore Packages** (if needed):
   - Right-click solution → "Restore NuGet Packages"

3. **Run**:
   - Press **F5** or click "Start" button
   - The app will build and launch

### Method 3: Build and Run Executable

1. **Build Release**:
   ```bash
   dotnet build -c Release
   ```

2. **Run Executable**:
   ```bash
   cd ClearVolumeWindows\bin\Release\net6.0-windows
   .\ClearVolume.exe
   ```

---

## First Run Checklist

✅ **Camera Permission**: Windows may ask for camera permission - click "Allow"

✅ **Camera Connected**: Make sure your webcam/camera is connected

✅ **Point at Bottle**: Point camera at a clear bottle

✅ **Good Lighting**: Ensure good lighting for best results

✅ **Wait for Detection**: May take 1-2 seconds to detect bottle

---

## Expected Behavior

1. **App launches**: Window opens with black background
2. **Camera activates**: Camera preview appears (may take 1-2 seconds)
3. **Guidance message**: Shows "Point camera at clear bottle"
4. **Bottle detected**: Guidance hides, volume estimate appears
5. **Volume updates**: Updates in real-time as camera moves

---

## Troubleshooting

### ❌ "Camera not found" or "Failed to open camera"

**Solution**:
1. Check camera is connected
2. Test camera in other apps (Camera app, Teams, etc.)
3. Restart computer if needed
4. Check Device Manager for camera

### ❌ "Camera access denied"

**Solution**:
1. Windows Settings → Privacy → Camera
2. Enable "Allow apps to access your camera"
3. Restart app

### ❌ No volume estimate appears

**Solution**:
1. **Improve lighting**: Use bright, even lighting
2. **Move closer**: Position camera 30-50cm from bottle
3. **Clear view**: Ensure bottle is fully visible
4. **Wait**: Detection may take 1-2 seconds
5. **Use clear bottles**: Works best with transparent bottles

### ❌ App crashes on startup

**Solution**:
1. Check error message in console
2. Verify .NET 6.0 SDK is installed: `dotnet --version`
3. Restore packages: `dotnet restore`
4. Rebuild: `dotnet clean` then `dotnet build`

---

## Performance Tips

- **Close other camera apps**: Only one app can access camera at a time
- **Good lighting**: Improves detection accuracy
- **Steady camera**: Hold camera steady for better detection
- **Clear background**: Use contrasting background

---

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

# Build release
dotnet build -c Release

# Clean build
dotnet clean
dotnet build
```

---

## Need Help?

1. Check error messages in console
2. Review `SETUP.md` for detailed setup
3. Review `README.md` for documentation
4. Check Windows Event Viewer for errors
5. Verify all requirements are met

---

**Happy measuring! 🥤**

