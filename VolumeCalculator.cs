using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace ClearVolumeWindows
{
    public class VolumeCalculator
    {
        // Bottle specifications database
        private readonly Dictionary<string, BottleSpec> _bottleSpecs = new Dictionary<string, BottleSpec>
        {
            ["water_bottle_500ml"] = new BottleSpec(height: 0.22, diameter: 0.065, totalVolume: 500),
            ["water_bottle_1000ml"] = new BottleSpec(height: 0.28, diameter: 0.075, totalVolume: 1000),
            ["soda_bottle_500ml"] = new BottleSpec(height: 0.23, diameter: 0.065, totalVolume: 500),
            ["generic"] = new BottleSpec(height: 0.25, diameter: 0.070, totalVolume: 750) // Default
        };

        public VolumeResult CalculateVolume(string bottleType, double fluidLevel, Rect bottleRect, 
            int imageWidth, int imageHeight)
        {
            // Get bottle specifications
            var spec = _bottleSpecs.ContainsKey(bottleType) 
                ? _bottleSpecs[bottleType] 
                : _bottleSpecs["generic"];

            // Estimate real-world dimensions from image
            // Simplified: assume bottle fills a certain percentage of image height
            // In a real implementation, you'd use camera calibration and depth estimation
            var imageHeightMeters = 0.5; // Assume image represents ~0.5m field of view
            var scaleFactor = imageHeightMeters / imageHeight;
            
            var estimatedHeight = bottleRect.Height * scaleFactor;
            var estimatedDiameter = (bottleRect.Width / bottleRect.Height) * estimatedHeight * 0.3; // Aspect ratio adjustment

            // Use known bottle specs if available, otherwise use estimated
            var bottleHeight = spec.Height;
            var bottleDiameter = spec.Diameter;

            // Calculate volume based on fluid level (0.0 = empty, 1.0 = full)
            var fluidHeight = bottleHeight * fluidLevel;
            var volume = CalculateCylindricalVolume(fluidHeight, bottleDiameter);

            // Adjust for bottle shape (most bottles taper at top/bottom)
            var adjustedVolume = ApplyBottleShapeCorrection(volume, fluidLevel, spec);

            // Calculate confidence based on detection quality
            var confidence = CalculateConfidence(bottleRect, fluidLevel, imageWidth, imageHeight);

            return new VolumeResult
            {
                Volume = adjustedVolume,
                Confidence = confidence,
                BottleType = bottleType
            };
        }

        private double CalculateCylindricalVolume(double height, double diameter)
        {
            var radius = diameter / 2.0;
            var volumeM3 = Math.PI * radius * radius * height;
            var volumeML = volumeM3 * 1_000_000; // Convert to milliliters
            return volumeML;
        }

        private double ApplyBottleShapeCorrection(double volume, double fluidLevel, BottleSpec spec)
        {
            // Apply correction for bottle tapering
            // Most bottles are narrower at top and bottom
            double correctionFactor = 1.0;

            if (fluidLevel < 0.1)
            {
                // Bottom taper (narrower)
                correctionFactor = 0.85;
            }
            else if (fluidLevel > 0.9)
            {
                // Top taper (narrower, plus neck)
                correctionFactor = 0.75;
            }
            else
            {
                // Middle section (more cylindrical)
                correctionFactor = 0.95;
            }

            return volume * correctionFactor;
        }

        private double CalculateConfidence(Rect bottleRect, double fluidLevel, int imageWidth, int imageHeight)
        {
            double confidence = 0.5;

            // Larger detection area = higher confidence
            var area = (double)(bottleRect.Width * bottleRect.Height) / (imageWidth * imageHeight);
            if (area > 0.3)
            {
                confidence += 0.2;
            }
            else if (area > 0.15)
            {
                confidence += 0.1;
            }

            // Valid fluid level range
            if (fluidLevel >= 0.0 && fluidLevel <= 1.0)
            {
                confidence += 0.2;
            }

            // Detection stability (would be improved with tracking)
            confidence += 0.1;

            return Math.Min(confidence, 1.0);
        }
    }

    public class BottleSpec
    {
        public double Height { get; } // meters
        public double Diameter { get; } // meters
        public double TotalVolume { get; } // milliliters

        public BottleSpec(double height, double diameter, double totalVolume)
        {
            Height = height;
            Diameter = diameter;
            TotalVolume = totalVolume;
        }
    }
}

