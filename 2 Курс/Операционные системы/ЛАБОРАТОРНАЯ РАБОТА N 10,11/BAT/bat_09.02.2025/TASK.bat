@echo off
setlocal

if "%~1"=="" (
    echo No arguments were given
    exit /b 1
)

set "EXT=%~1"
if /I "%EXT%" NEQ "cmd" if /I "%EXT%" NEQ "bat" (
    echo ERROR: Only cmd and bat ext are avaible
    exit /b 1
)

for /f %%I in ('powershell -NoProfile -Command "Get-Date -Format 'dd.MM.yyyy'"') do set DATE=%%I

set "FOLDER=%EXT%_%DATE%"
if not exist "%FOLDER%" mkdir "%FOLDER%"

move "*.%EXT%" "%FOLDER%" >nul 2>&1

if %errorlevel% NEQ 0 (
    echo There are no .%EXT% files to move
    exit /b 1
)

echo files .%EXT% moved to %FOLDER%
exit /b 0
