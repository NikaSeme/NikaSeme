using System;
using OpenCvSharp;

namespace ClearVolumeWindows
{
    public class BottleDetector
    {
        public BottleDetection? DetectBottle(Mat image)
        {
            try
            {
                // Convert to grayscale
                var gray = new Mat();
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                // Apply Gaussian blur
                var blurred = new Mat();
                Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 0);

                // Edge detection
                var edges = new Mat();
                Cv2.Canny(blurred, edges, 50, 150);

                // Find contours
                Cv2.FindContours(edges, out Point[][] contours, out HierarchyIndex[] hierarchy, 
                    RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // Find the largest rectangular contour (likely the bottle)
                Rect? bestRect = null;
                double maxArea = 0;
                double minArea = image.Width * image.Height * 0.05; // At least 5% of image

                foreach (var contour in contours)
                {
                    var area = Cv2.ContourArea(contour);
                    
                    if (area < minArea)
                    {
                        continue;
                    }

                    // Approximate contour to polygon
                    var epsilon = 0.02 * Cv2.ArcLength(contour, true);
                    var approx = Cv2.ApproxPolyDP(contour, epsilon, true);

                    // Check if it's roughly rectangular (4 corners)
                    if (approx.Length >= 4)
                    {
                        // Get bounding rectangle
                        var rect = Cv2.BoundingRect(contour);
                        
                        // Check aspect ratio (bottles are typically tall)
                        var aspectRatio = (double)rect.Height / rect.Width;
                        
                        if (aspectRatio > 1.2 && aspectRatio < 4.0 && area > maxArea)
                        {
                            maxArea = area;
                            bestRect = rect;
                        }
                    }
                }

                // Cleanup
                gray.Dispose();
                blurred.Dispose();
                edges.Dispose();

                if (bestRect.HasValue)
                {
                    return new BottleDetection
                    {
                        IsDetected = true,
                        BoundingBox = bestRect.Value,
                        BottleType = "generic",
                        Confidence = Math.Min(1.0, maxArea / (image.Width * image.Height * 0.3))
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bottle detection error: {ex.Message}");
                return null;
            }
        }
    }
}

