[Setup]
AppName=The Legendary Sword
AppVersion=1.0.0
DefaultDirName={autopf}\The Legendary Sword
DefaultGroupName=The Legendary Sword
OutputBaseFilename=TheLegendarySwordSetup
OutputDir=.
Compression=lzma
SolidCompression=yes
AppPublisher=The Legendary Sword
AppPublisherURL=https://gustavochavez.dev/
AppSupportURL=https://gustavochavez.dev/legendary-sword
AppUpdatesURL=https://gustavochavez.dev/legendary-sword
PrivilegesRequired=lowest

[Files]
Source: "artifacts\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\res\*"; DestDir: "{app}\res"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\The Legendary Sword"; Filename: "{app}\SwordEngine.exe"
Name: "{userdesktop}\The Legendary Sword"; Filename: "{app}\SwordEngine.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; Flags: unchecked

[Run]
Filename: "{app}\SwordEngine.exe"; Description: "Launch The Legendary Sword"; Flags: nowait postinstall skipifsilent
