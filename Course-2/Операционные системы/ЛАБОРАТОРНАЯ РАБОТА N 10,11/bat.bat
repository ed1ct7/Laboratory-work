@echo off
if "%~1"=="" (
    echo No arguments were given
    exit /b 1
)
if not exist "%~1" (
    echo Error
    exit /b 1
)
set "output_file=files_list.txt"
dir /s /b %1 > "%output_file%"
echo created %output_file%
