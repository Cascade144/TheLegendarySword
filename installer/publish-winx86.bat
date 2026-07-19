@echo off
setlocal

rem Publish the SwordEngine project as a self-contained single-file exe (x86)
dotnet publish ..\src\SwordEngine\SwordEngine.csproj -c Release -r win-x86 --self-contained true /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true -o artifacts\publish-win-x86
if %ERRORLEVEL% neq 0 (
  echo Publish failed.
  pause
  exit /b %ERRORLEVEL%
)

echo Published to artifacts\publish-win-x86
pause
