@echo off
setlocal

if "%~1"=="" (
    echo Использование: move_files.bat ^<расширение^>
    exit /b 1
)

set "EXT=%~1"

for /f %%I in ('powershell -NoProfile -Command "Get-Date -Format 'dd.MM.yyyy'"') do set DATE=%%I

set "FOLDER=%EXT%_%DATE%"
if not exist "%FOLDER%" mkdir "%FOLDER%"

move "*.%EXT%" "%FOLDER%" >nul 2>&1

if %errorlevel% NEQ 0 (
    echo Нет файлов с расширением .%EXT% для перемещения.
    exit /b 1
)

echo Файлы .%EXT% перемещены в папку %FOLDER%
exit /b 0
