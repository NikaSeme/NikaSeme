# Gitignore File - iCloud Compatibility

## Issue
iCloud may have issues with files that start with a dot (`.gitignore`), as these are typically hidden files in Unix-like systems.

## Solution
This folder contains `gitignore.txt` which is iCloud-compatible. The file contains the same content as `.gitignore` but with a `.txt` extension so iCloud can sync it properly.

## How to Use

### Option 1: Use gitignore.txt (Recommended for iCloud)
- Keep `gitignore.txt` as is
- Git will still respect it if you configure it (though `.gitignore` is standard)

### Option 2: Rename for Git (If you use Git)
If you want to use Git and need the standard `.gitignore` file:

1. **After downloading/cloning**, rename `gitignore.txt` to `.gitignore`:
   ```bash
   mv gitignore.txt .gitignore
   ```

2. **Or in File Explorer (Windows)**:
   - Right-click `gitignore.txt`
   - Select "Rename"
   - Change to `.gitignore`
   - You may need to enable "Show file extensions" in View settings

### Option 3: Keep Both
- Keep `gitignore.txt` for iCloud syncing
- Create `.gitignore` locally for Git
- Both files can coexist

## Note
- The `.gitignore` file (if it exists) may not sync properly with iCloud
- The `gitignore.txt` file will sync properly with iCloud
- Git will work with either name, but `.gitignore` is the standard convention

## Contents
Both files contain the same ignore patterns for:
- Visual Studio build files
- User settings
- Build results
- NuGet packages
- OpenCV libraries
- And more...

