ECHO Starting PreBuild.bat
REM Usage: Call "$(MSBuildProjectDirectory)\PreBuild.$(ConfigurationName).bat" "$(MSBuildProjectDirectory)" "$(ConfigurationName)"
REM Vars:  $(ProjectName) = MyCo.Framework. Models, $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug, $(ConfigurationName) = "Debug"

REM Locals
SET FullPath=%1
SET FullPath=%FullPath:"=%
SET ProductFolder="framework.dataaccess"

ECHO ** PreBuild.bat **
ECHO FullPath: %FullPath%
SET Configuration=%2
ECHO Configuration: %Configuration%



ECHO Executing "%FullPath%\EFPartial.ps1" -Parameter1 "%FullPath%"

%WINDIR%\system32\attrib.exe "%FullPath%\*.cs" -r
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe Set-ExecutionPolicy Bypass -Scope Process -Force
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe -File "%FullPath%\EFPartial.ps1" -Parameter1 "%FullPath%"
%WINDIR%\system32\attrib.exe "%FullPath%\*.cs" +r

exit 0