using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ClearVolumeWindows
{
    public class CameraManager : IDisposable
    {
        private VideoCapture? _videoCapture;
        private Mat? _frame;
        private bool _isCapturing = false;
        private DispatcherTimer? _captureTimer;
        private readonly object _lockObject = new object();

        public event EventHandler<FrameEventArgs>? FrameAvailable;

        public bool IsInitialized { get; private set; }

        public async Task<bool> RequestCameraAccessAsync()
        {
            // On Windows, camera access is typically granted automatically
            // For UWP apps, you'd need to request permission
            // For WPF, we assume access is granted if camera is available
            return await Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    // Try to open default camera (index 0)
                    _videoCapture = new VideoCapture(0);
                    
                    if (!_videoCapture.IsOpened())
                    {
                        throw new Exception("Failed to open camera. Make sure a camera is connected.");
                    }

                    // Set camera properties
                    _videoCapture.Set(VideoCaptureProperties.FrameWidth, 1280);
                    _videoCapture.Set(VideoCaptureProperties.FrameHeight, 720);
                    _videoCapture.Set(VideoCaptureProperties.Fps, 30);

                    _frame = new Mat();
                    IsInitialized = true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Camera initialization failed: {ex.Message}", ex);
                }
            });
        }

        public async Task StartCaptureAsync()
        {
            if (!IsInitialized || _videoCapture == null)
            {
                throw new InvalidOperationException("Camera not initialized");
            }

            if (_isCapturing)
            {
                return;
            }

            _isCapturing = true;

            // Start capture timer on UI thread
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                _captureTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(33) // ~30 FPS
                };
                _captureTimer.Tick += CaptureFrame;
                _captureTimer.Start();
            });
        }

        private void CaptureFrame(object? sender, EventArgs e)
        {
            if (!_isCapturing || _videoCapture == null)
            {
                return;
            }

            lock (_lockObject)
            {
                if (_frame != null && _videoCapture.Read(_frame) && !_frame.Empty())
                {
                    // Convert Mat to BitmapSource
                    var bitmap = ImageConverter.MatToBitmapSource(_frame);

                    // Raise frame available event
                    FrameAvailable?.Invoke(this, new FrameEventArgs(bitmap));
                }
            }
        }

        public void StopCapture()
        {
            if (!_isCapturing)
            {
                return;
            }

            _isCapturing = false;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _captureTimer?.Stop();
                _captureTimer = null;
            });
        }

        public void Dispose()
        {
            StopCapture();
            _videoCapture?.Release();
            _videoCapture?.Dispose();
            _frame?.Dispose();
            _frame = null;
            IsInitialized = false;
        }
    }

    public class FrameEventArgs : EventArgs
    {
        public BitmapSource Frame { get; }

        public FrameEventArgs(BitmapSource frame)
        {
            Frame = frame;
        }
    }
}

