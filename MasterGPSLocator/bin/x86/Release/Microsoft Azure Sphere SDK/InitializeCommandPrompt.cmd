@echo off
REM Set up a Developer Command Prompt for Azure Sphere tools.
cls
PATH=%~dp0;%~dp0\Tools;%~dp0\SysRoot\tools\gcc;%PATH%
