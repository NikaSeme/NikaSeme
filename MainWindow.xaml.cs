using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace ClearVolumeWindows
{
    public partial class MainWindow : Window
    {
        private CameraManager? _cameraManager;
        private VolumeEstimator? _volumeEstimator;
        private DispatcherTimer? _updateTimer;
        private int _frameCount = 0;
        private DateTime _lastFpsUpdate = DateTime.Now;

        public double FrameRate { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeApp();
        }

        private async void InitializeApp()
        {
            try
            {
                // Initialize camera manager
                _cameraManager = new CameraManager();
                _volumeEstimator = new VolumeEstimator();
                
                if (_cameraManager == null || _volumeEstimator == null)
                {
                    StatusText.Text = "Failed to initialize";
                    return;
                }

                // Request camera access
                StatusText.Text = "Requesting camera access...";
                bool hasAccess = await _cameraManager.RequestCameraAccessAsync();
                
                if (!hasAccess)
                {
                    StatusText.Text = "Camera access denied. Please grant camera permission in Windows Settings.";
                    MessageBox.Show("Camera access is required for ClearVolume to work. Please enable camera access in Windows Settings.",
                        "Camera Access Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Initialize camera
                StatusText.Text = "Initializing camera...";
                await _cameraManager.InitializeAsync();

                // Start frame processing
                if (_cameraManager != null)
                {
                    _cameraManager.FrameAvailable += OnFrameAvailable;
                    await _cameraManager.StartCaptureAsync();
                }

                // Start UI update timer
                _updateTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(33) // ~30 FPS
                };
                _updateTimer.Tick += UpdateUI;
                _updateTimer.Start();

                StatusText.Text = "Ready - Point camera at clear bottle";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
                MessageBox.Show($"Failed to initialize camera: {ex.Message}",
                    "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OnFrameAvailable(object? sender, FrameEventArgs e)
        {
            try
            {
                // Update camera preview
                await Dispatcher.InvokeAsync(() =>
                {
                    CameraPreview.Source = e.Frame;
                    _frameCount++;
                });

                // Process frame for volume estimation (every 5th frame for performance)
                if (_frameCount % 5 == 0)
                {
                    await ProcessFrameAsync(e.Frame);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Frame processing error: {ex.Message}");
            }
        }

        private async Task ProcessFrameAsync(BitmapSource frame)
        {
            try
            {
                if (_volumeEstimator == null) return;
                
                var result = await _volumeEstimator.EstimateVolumeAsync(frame);
                
                await Dispatcher.InvokeAsync(() =>
                {
                    if (result != null && result.Volume.HasValue)
                    {
                        // Show volume display - centered at bottom
                        VolumeDisplay.Visibility = Visibility.Visible;
                        VolumeText.Text = ((int)result.Volume.Value).ToString();
                    }
                    else
                    {
                        // Hide volume display when no bottle detected
                        VolumeDisplay.Visibility = Visibility.Collapsed;
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Volume estimation error: {ex.Message}");
            }
        }

        private void UpdateUI(object? sender, EventArgs e)
        {
            // Update FPS
            var now = DateTime.Now;
            var elapsed = (now - _lastFpsUpdate).TotalSeconds;
            if (elapsed >= 1.0)
            {
                FrameRate = _frameCount / elapsed;
                FpsText.Text = $"FPS: {FrameRate:F1}";
                _frameCount = 0;
                _lastFpsUpdate = now;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_cameraManager != null)
            {
                _cameraManager.StopCapture();
                _cameraManager.Dispose();
            }
            _updateTimer?.Stop();
            base.OnClosing(e);
        }
    }
}

