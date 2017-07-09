ECHO Starting PreBuild.bat
REM Note: Beware that .bat files in VS have junk characters at beginning that must be removed via Binary Editor.
REM PreBuildEvent: Call $(ProjectDir)PreBuild.Bat $(ProjectDir)

REM Locals
SET FullPath=%1
SET FullPath=%FullPath:"=%

ECHO ** PreBuild.bat **
ECHO Executing "%FullPath%EFPartial.ps1" -Parameter1 "%FullPath%"

%WINDIR%\system32\attrib.exe "%FullPath%*.cs" -r
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe Set-ExecutionPolicy Bypass -Scope CurrentUser -Force
%WINDIR%\SysWOW64\WindowsPowerShell\v1.0\Powershell.exe -File "%FullPath%EFPartial.ps1" -Parameter1 "%FullPath%"
%WINDIR%\system32\attrib.exe "%FullPath%*.cs" +r
exit 0