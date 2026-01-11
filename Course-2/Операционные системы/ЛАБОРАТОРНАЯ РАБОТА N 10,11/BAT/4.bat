@echo off
:start
cls
if "%1"=="" goto end
echo Пакетный файл %1
copy c:\bat\%1.bat con
pause
shift
goto start
:end