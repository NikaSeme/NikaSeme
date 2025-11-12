using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ClearVolumeWindows
{
    public class VolumeEstimator
    {
        private readonly VolumeCalculator _volumeCalculator;
        private readonly BottleDetector _bottleDetector;

        public VolumeEstimator()
        {
            _volumeCalculator = new VolumeCalculator();
            _bottleDetector = new BottleDetector();
        }

        public async Task<VolumeResult?> EstimateVolumeAsync(BitmapSource frame)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Convert BitmapSource to OpenCV Mat
                    var mat = ImageConverter.BitmapSourceToMat(frame);

                    // Detect bottle in frame
                    var bottleDetection = _bottleDetector.DetectBottle(mat);
                    
                    if (bottleDetection == null || !bottleDetection.IsDetected)
                    {
                        return null;
                    }

                    // Detect fluid level
                    var fluidLevel = DetectFluidLevel(mat, bottleDetection.BoundingBox);
                    
                    if (!fluidLevel.HasValue)
                    {
                        return null;
                    }

                    // Calculate volume
                    var volume = _volumeCalculator.CalculateVolume(
                        bottleType: bottleDetection.BottleType,
                        fluidLevel: fluidLevel.Value,
                        bottleRect: bottleDetection.BoundingBox,
                        imageWidth: mat.Width,
                        imageHeight: mat.Height
                    );

                    mat.Dispose();
                    return volume;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Volume estimation error: {ex.Message}");
                    return null;
                }
            });
        }

        private double? DetectFluidLevel(Mat image, Rect bottleRect)
        {
            try
            {
                // Crop to bottle region
                var roi = new Rect(
                    Math.Max(0, bottleRect.X),
                    Math.Max(0, bottleRect.Y),
                    Math.Min(bottleRect.Width, image.Width - bottleRect.X),
                    Math.Min(bottleRect.Height, image.Height - bottleRect.Y)
                );

                if (roi.Width <= 0 || roi.Height <= 0)
                {
                    return null;
                }

                var bottleRegion = new Mat(image, roi);

                // Convert to grayscale
                var gray = new Mat();
                Cv2.CvtColor(bottleRegion, gray, ColorConversionCodes.BGR2GRAY);

                // Apply Gaussian blur
                var blurred = new Mat();
                Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 0);

                // Edge detection
                var edges = new Mat();
                Cv2.Canny(blurred, edges, 50, 150);

                // Find horizontal lines (fluid level)
                LineSegmentPolar[] lines = Cv2.HoughLines(edges, 1, Math.PI / 180, 50);

                // Find the most prominent horizontal line within bottle region
                double? fluidLevelY = null;
                double maxLength = 0;

                if (lines != null && lines.Length > 0)
                {
                    foreach (var line in lines)
                    {
                        var rho = line.Rho;
                        var theta = line.Theta;

                        // Check if line is approximately horizontal (theta close to 0 or PI)
                        if (Math.Abs(theta) < 0.1 || Math.Abs(theta - Math.PI) < 0.1)
                        {
                            // Calculate y position in image coordinates
                            var sinTheta = Math.Sin(theta);
                            if (Math.Abs(sinTheta) > 0.001)
                            {
                                var y = (int)(rho / sinTheta) - roi.Y;

                                // Check if line is within bottle bounds
                                if (y >= 0 && y < bottleRect.Height)
                                {
                                    // Estimate line length (simplified)
                                    var length = bottleRect.Width;
                                    if (length > maxLength)
                                    {
                                        maxLength = length;
                                        fluidLevelY = y;
                                    }
                                }
                            }
                        }
                    }
                }

                // If no horizontal line found, use edge detection fallback
                if (!fluidLevelY.HasValue)
                {
                    // Use simplified method: find horizontal edge with most pixels
                    fluidLevelY = FindHorizontalEdge(edges, bottleRect.Height);
                }

                // Cleanup
                gray.Dispose();
                blurred.Dispose();
                edges.Dispose();
                bottleRegion.Dispose();

                // Normalize to 0-1 (0 = top, 1 = bottom)
                if (fluidLevelY.HasValue && bottleRect.Height > 0)
                {
                    var normalizedLevel = 1.0 - (fluidLevelY.Value / bottleRect.Height);
                    return Math.Max(0.0, Math.Min(1.0, normalizedLevel));
                }

                // Fallback: return middle value
                return 0.5;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fluid level detection error: {ex.Message}");
                return 0.5; // Return default middle value
            }
        }

        private double? FindHorizontalEdge(Mat edges, int height)
        {
            try
            {
                // Find row with most edge pixels
                int maxEdges = 0;
                int bestRow = height / 2; // Default to middle

                for (int y = height / 4; y < 3 * height / 4; y++)
                {
                    if (y >= 0 && y < edges.Height)
                    {
                        var row = edges.Row(y);
                        var edgeCount = Cv2.CountNonZero(row);
                        
                        if (edgeCount > maxEdges)
                        {
                            maxEdges = edgeCount;
                            bestRow = y;
                        }
                    }
                }

                return bestRow;
            }
            catch
            {
                return height / 2; // Return default middle value
            }
        }
    }

    public class VolumeResult
    {
        public double? Volume { get; set; } // in milliliters
        public double Confidence { get; set; } // 0.0 to 1.0
        public string? BottleType { get; set; }
    }

    public class BottleDetection
    {
        public bool IsDetected { get; set; }
        public Rect BoundingBox { get; set; }
        public string BottleType { get; set; } = "generic";
        public double Confidence { get; set; }
    }
}

