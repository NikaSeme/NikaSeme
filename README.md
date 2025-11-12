# ClearVolume Windows App

ClearVolume is a Windows desktop application that uses computer vision to estimate the remaining liquid volume in clear bottles through real-time camera analysis.

## Features

- **Real-time Volume Estimation**: Live display showing milliliters of liquid remaining
- **Automatic Bottle Recognition**: Detects bottle shape and size automatically
- **Precision Calculation**: Uses OpenCV for computer vision and volume calculations
- **User Guidance**: On-screen prompts to help align bottles for best results
- **No Account Required**: Privacy-focused, no login needed
- **Offline Functionality**: Works without internet connection

## Requirements

- Windows 10 or later
- .NET 6.0 or later
- Webcam or camera connected to computer
- Visual Studio 2022 (for building from source)

## Quick Start

### Option 1: Build from Source

1. **Prerequisites**:
   - Install [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
   - Install [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community edition is fine)
   - Install [OpenCV Runtime](https://opencv.org/releases/) (optional, NuGet package handles this)

2. **Clone or Download**:
   ```bash
   git clone <repository-url>
   cd ClearVolumeWindows
   ```

3. **Build**:
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run**:
   ```bash
   dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj
   ```

### Option 2: Visual Studio

1. Open `ClearVolumeWindows.sln` in Visual Studio 2022
2. Restore NuGet packages (right-click solution → Restore NuGet Packages)
3. Build solution (F6)
4. Run (F5)

## Project Structure

```
ClearVolumeWindows/
├── ClearVolumeWindows/
│   ├── App.xaml              # Application definition
│   ├── App.xaml.cs           # Application code-behind
│   ├── MainWindow.xaml       # Main window UI
│   ├── MainWindow.xaml.cs    # Main window logic
│   ├── CameraManager.cs      # Camera capture management
│   ├── VolumeEstimator.cs    # Volume estimation logic
│   ├── BottleDetector.cs     # Bottle detection using OpenCV
│   ├── VolumeCalculator.cs   # Volume calculations
│   ├── SettingsWindow.xaml   # Settings window UI
│   ├── SettingsWindow.xaml.cs # Settings window logic
│   └── Styles/
│       └── AppStyles.xaml    # Application styles
├── ClearVolumeWindows.sln    # Solution file
└── README.md                 # This file
```

## Usage

1. **Launch Application**: Run the executable or build from source
2. **Camera Access**: Grant camera permission when prompted (Windows may ask)
3. **Point Camera**: Point your camera at a clear bottle
4. **View Estimate**: Volume estimate appears on screen in milliliters
5. **Guidance**: Follow on-screen prompts for best results

## Technical Details

### Technologies Used

- **WPF (Windows Presentation Foundation)**: User interface
- **OpenCVSharp**: Computer vision and image processing
- **.NET 6.0**: Runtime and framework
- **C#**: Programming language

### Volume Estimation Pipeline

1. **Frame Capture**: Camera captures frames at ~30 FPS
2. **Bottle Detection**: OpenCV detects bottle contours in frame
3. **Fluid Level Detection**: Edge detection finds liquid surface
4. **Dimension Estimation**: Estimates real-world dimensions from image
5. **Volume Calculation**: Applies cylindrical volume formula with corrections
6. **Confidence Scoring**: Calculates accuracy estimate
7. **UI Update**: Displays volume estimate on screen

### Detection Methods

- **Contour Detection**: Uses OpenCV to find bottle contours
- **Edge Detection**: Canny edge detection for fluid level
- **Hough Lines**: Detects horizontal lines (fluid level)
- **Aspect Ratio Filtering**: Filters detections by bottle-like aspect ratios

## Known Limitations

- **Camera Quality**: Works best with good quality cameras
- **Lighting**: Requires good lighting for accurate detection
- **Bottle Type**: Works best with clear bottles
- **Distance**: Optimal distance is 30-50cm from bottle
- **Angle**: Works best when camera is directly facing bottle
- **Accuracy**: Volume estimates are approximate (within ~10-20% of actual)

## Troubleshooting

### Camera Not Working

1. **Check Camera Permission**:
   - Go to Windows Settings → Privacy → Camera
   - Ensure "Allow apps to access your camera" is enabled
   - Ensure ClearVolume has camera access

2. **Check Camera Connection**:
   - Verify camera is connected and working
   - Test camera in other applications
   - Restart application if needed

### No Volume Estimate

1. **Improve Lighting**: Use bright, even lighting
2. **Move Closer**: Position camera 30-50cm from bottle
3. **Clear View**: Ensure bottle is fully visible in frame
4. **Wait**: Detection may take 1-2 seconds

### Application Crashes

1. **Check .NET Version**: Ensure .NET 6.0 or later is installed
2. **Check OpenCV**: Verify OpenCV runtime is available
3. **Check Logs**: Review error messages in console
4. **Restart**: Close and reopen application

### Poor Accuracy

1. **Lighting**: Improve lighting conditions
2. **Distance**: Adjust camera distance (30-50cm optimal)
3. **Angle**: Point camera directly at bottle
4. **Bottle Type**: Use clear bottles for best results
5. **Stability**: Hold camera steady

## Development

### Building from Source

```bash
# Restore dependencies
dotnet restore

# Build project
dotnet build

# Run application
dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj

# Build release
dotnet build -c Release
```

### Dependencies

- **OpenCvSharp4**: Computer vision library
- **OpenCvSharp4.runtime.win**: OpenCV runtime for Windows
- **Microsoft.ML**: Machine learning framework (optional, for future ML model)
- **Microsoft.ML.OnnxRuntime**: ONNX runtime (optional, for future ML model)

### Adding ML Model

To improve accuracy, you can add a trained ML model:

1. Train model using TensorFlow/PyTorch
2. Convert to ONNX format
3. Load model in `BottleDetector.cs`
4. Use model for detection instead of OpenCV contours

See `ML_Training_Guide.md` for details (if available).

## Performance

### Targets

- **Frame Rate**: 30 FPS (processing every 5th frame)
- **Response Time**: < 1 second for volume estimate
- **CPU Usage**: < 20% on modern CPUs
- **Memory Usage**: < 200 MB

### Optimization

- Frame processing is done asynchronously
- Only every 5th frame is processed for volume estimation
- OpenCV operations are optimized for performance
- UI updates are throttled to 30 FPS

## Future Improvements

- [ ] Add ML model for better bottle detection
- [ ] Improve fluid level detection accuracy
- [ ] Add depth estimation using stereo vision
- [ ] Support multiple bottle types
- [ ] Add calibration system
- [ ] Improve UI/UX
- [ ] Add settings for camera selection
- [ ] Add volume history tracking
- [ ] Add export functionality

## License

[Add your license here]

## Contributors

[Add contributors here]

## Support

For issues or questions:
1. Check this README
2. Review error messages
3. Check Windows camera settings
4. Verify all requirements are met
5. Open an issue on GitHub (if applicable)

## Acknowledgments

- OpenCV community for computer vision tools
- .NET community for framework and tools
- Windows team for platform support

---

**Version**: 1.0.0
**Last Updated**: [Current Date]
**Status**: Active Development

