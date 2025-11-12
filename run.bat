@echo off
echo ClearVolume Windows - Quick Run Script
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 6.0 SDK is not installed!
    echo Please install from: https://dotnet.microsoft.com/download/dotnet/6.0
    pause
    exit /b 1
)

echo .NET SDK found!
echo.

REM Navigate to project directory
cd /d "%~dp0"

echo Restoring NuGet packages...
call dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo Building project...
call dotnet build
if %errorlevel% neq 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)

echo.
echo Running application...
echo.
call dotnet run --project ClearVolumeWindows/ClearVolumeWindows.csproj

pause

