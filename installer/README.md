# Installer

This folder contains the installer assets and build helpers for The Legendary Sword.

## Requirements
- Install the .NET 8 SDK for Windows if you want to publish the game from source.
- Install Inno Setup if you want to compile the installer script.

## Publish for a target platform
Use one of the provided batch files from this folder to publish the game for a specific runtime:

- Windows x64: publish-winx64.bat
- Windows x86: publish-winx86.bat

Each script publishes the project to a platform-specific output folder under artifacts.

## Build the installer
1. Publish the game build for the platform you want to package.
2. Compile the Inno Setup script with ISCC from your Inno Setup installation:
   ISCC installer\\TheLegendarySword.iss
3. The resulting installer will be created here as TheLegendarySwordSetup.exe.

## Install the game
1. Run the generated TheLegendarySwordSetup.exe on a Windows machine.
2. Follow the on-screen prompts to install The Legendary Sword.
3. The installer copies the self-contained build and assets into Program Files, so the target machine does not need the .NET SDK installed.

## Output
- The installer is generated into this folder by the Inno Setup script.
- It installs the game executable and its required assets under Program Files.
