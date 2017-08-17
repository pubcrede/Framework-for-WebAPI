ECHO OFF
REM Usage: Call "$(ProjectDir)PreBuild.$(ConfigurationName).bat" "$(ProjectDir)" "$(ConfigurationName)"
REM Vars:  $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug, $(ConfigurationName) = "Debug"

ECHO Starting PreBuild.bat

REM Locals
SET FullPath=%1
SET FullPath=%FullPath:"=%
ECHO FullPath: %FullPath%
SET Configuration=%2
ECHO Configuration: %Configuration%

if "%Configuration%"=="" SET Configuration="Debug"

ECHO Executing "%FullPath%EFPartial.ps1" -Parameter1 "%FullPath%"

%WINDIR%\system32\attrib.exe "%FullPath%*.cs" -r
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe Set-ExecutionPolicy Bypass -Scope Process -Force
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe -File "%FullPath%EFPartial.ps1" -Parameter1 "%FullPath%"
%WINDIR%\system32\attrib.exe "%FullPath%*.cs" +r

Exit 0
