# Installer

This folder contains the installer assets and build helpers for The Legendary Sword.

## Publish for a target platform
Use one of the provided batch files from this folder to publish the game for a specific runtime:

- Windows x64: publish-winx64.bat
- Windows x86: publish-win-x86.bat

Each script publishes the project to a platform-specific output folder under artifacts.

## Build the installer
1. Install Inno Setup on your machine if it is not already available.
2. Publish the game build for the platform you want to package.
3. Compile the Inno Setup script with ISCC from your Inno Setup installation:
   ISCC installer\\TheLegendarySword.iss
4. The resulting installer will be created here as TheLegendarySwordSetup.exe.

## Run the installer
- After the setup file is generated, run the resulting installer executable from your file explorer or terminal.
- Follow the on-screen prompts to install The Legendary Sword.

## Output
- The installer is generated into this folder by the Inno Setup script.
- It installs the game executable and its required assets under Program Files.
