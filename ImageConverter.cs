using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using OpenCvSharp;

namespace ClearVolumeWindows
{
    public static class ImageConverter
    {
        public static BitmapSource MatToBitmapSource(Mat image)
        {
            if (image == null || image.Empty())
            {
                throw new ArgumentException("Image is null or empty");
            }

            try
            {
                // Convert Mat to byte array
                var imageBytes = image.ToBytes(".png");
                
                // Create BitmapImage from byte array
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(imageBytes);
                bitmap.EndInit();
                bitmap.Freeze();
                
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert Mat to BitmapSource: {ex.Message}", ex);
            }
        }

        public static Mat BitmapSourceToMat(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
            {
                throw new ArgumentNullException(nameof(bitmapSource));
            }

            try
            {
                // Convert BitmapSource to byte array
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    var imageBytes = stream.ToArray();
                    
                    // Create Mat from byte array
                    return Cv2.ImDecode(imageBytes, ImreadModes.Color);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert BitmapSource to Mat: {ex.Message}", ex);
            }
        }
    }
}

