@echo off
setlocal

rem Publish the SwordEngine project as a self-contained single-file exe (x64)
dotnet publish ..\src\SwordEngine\SwordEngine.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeAllContentForSelfExtract=true -o artifacts\publish
if %ERRORLEVEL% neq 0 (
  echo Publish failed.
  pause
  exit /b %ERRORLEVEL%
)

echo Published to artifacts\publish
pause
