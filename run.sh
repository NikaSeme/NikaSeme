#!/bin/bash

echo "ClearVolume Windows - Quick Run Script"
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET 6.0 SDK is not installed!"
    echo "Please install from: https://dotnet.microsoft.com/download/dotnet/6.0"
    exit 1
fi

echo ".NET SDK found!"
echo ""

# Navigate to project directory
cd "$(dirname "$0")"

echo "Restoring NuGet packages..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "ERROR: Failed to restore packages"
    exit 1
fi

echo ""
echo "Building project..."
dotnet build
if [ $? -ne 0 ]; then
    echo "ERROR: Build failed"
    exit 1
fi

echo ""
echo "Running application..."
echo ""
dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj

