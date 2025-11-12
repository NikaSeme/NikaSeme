# ClearVolume Windows - Quick Start Guide

Get started with ClearVolume Windows in 5 minutes!

## Prerequisites

- Windows 10 or later
- .NET 6.0 SDK or later
- Webcam or camera
- Visual Studio 2022 (optional, for building)

## Quick Setup

### Option 1: Build from Source (Recommended)

1. **Install .NET SDK**:
   ```bash
   # Download from https://dotnet.microsoft.com/download/dotnet/6.0
   # Verify installation:
   dotnet --version
   ```

2. **Clone/Download Project**:
   ```bash
   cd ClearVolumeWindows
   ```

3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

4. **Build Project**:
   ```bash
   dotnet build
   ```

5. **Run Application**:
   ```bash
   dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj
   ```

### Option 2: Visual Studio

1. **Open Solution**:
   - Open `ClearVolumeWindows.sln` in Visual Studio 2022
   - Restore NuGet packages (right-click solution → Restore NuGet Packages)

2. **Build**:
   - Press `F6` or Build → Build Solution

3. **Run**:
   - Press `F5` or Debug → Start Debugging

## First Run

1. **Launch Application**: Run the executable or build from source
2. **Grant Camera Permission**: Windows may ask for camera permission
3. **Point Camera**: Point your camera at a clear bottle
4. **View Estimate**: Volume estimate appears on screen in milliliters

## Usage Tips

### Best Results

1. **Lighting**: Use bright, even lighting
2. **Distance**: Keep camera 30-50cm from bottle
3. **Angle**: Point camera directly at bottle (not at angle)
4. **Background**: Use contrasting background
5. **Stability**: Hold camera steady for better detection
6. **Bottle Type**: Use clear bottles for best results

### Troubleshooting

#### Camera Not Working
- Check camera permission in Windows Settings
- Verify camera is connected and working
- Test camera in other applications
- Restart application if needed

#### No Volume Estimate
- Improve lighting conditions
- Move closer to bottle (30-50cm optimal)
- Ensure bottle is fully visible in frame
- Wait for detection (may take 1-2 seconds)

#### Poor Accuracy
- Improve lighting
- Adjust camera distance
- Point camera directly at bottle
- Use clear bottles

## Next Steps

1. **Test with Various Bottles**: Test with different bottle types and sizes
2. **Improve Detection**: Enhance bottle and fluid level detection
3. **Add ML Model**: Train and integrate ML model for better accuracy (see ML_Training_Guide.md)
4. **Optimize Performance**: Fine-tune frame processing for better performance

## Resources

- **Full Setup Guide**: See `SETUP.md`
- **Documentation**: See `README.md`
- **ML Training**: See `ML_Training_Guide.md` (if available)

## Support

For issues:
1. Check this quick start guide
2. Review `SETUP.md` for detailed setup
3. Check error messages in console
4. Verify all requirements are met

Happy measuring! 🥤

