# TheLegendarySword
A Windows arcade-style action game built with C# and SkiaSharp.
Originally this was a college project between me and my best friend, but it was written in Java, and used the awt.Canvas library.

I took the liberty of translating it to run in C# for fun on my own.
The original repo can be found here: https://github.com/MiguelC72/silver-carnival

All credits go to him for figuring out the hard math, as at the time (in college), I was still learning.
Couldn't have done it without him.

## Build and run
- Install the .NET 7 SDK or newer.
- From the repository root, run:
  - `dotnet publish src\SwordEngine\SwordEngine.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true -o artifacts\publish`
- The published game output will be placed in the `artifacts/publish` folder.

## Installer
- An Inno Setup installer script is available at `installer/TheLegendarySword.iss`.
- Building it produces a Windows installer in the `installer` folder.
- The packaged build includes the executable, runtime assets, and the game resources under `res`.

## Notes
