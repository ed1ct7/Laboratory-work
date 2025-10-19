echo off 
cls
echo start backup
if exist backup\4.bat del backup\4.bat 
dir backup
copy *.* backup
dir backup
echo endbackup
