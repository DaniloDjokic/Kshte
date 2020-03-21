:: This is a simple script to check the architecture of the system and run the appropriate setup file. The needed setup files need to be in the same folder as the script.

@echo OFF
setlocal

reg Query "HKLM\Hardware\Description\System\CentralProcessor\0" | find /i "x86" > NUL && set OS=32BIT || set OS=64BIT

if %OS%==32BIT start "" ".\KshteSetup_x86.msi"
if %OS%==64BIT start "" ".\KshteSetup_x64.msi"