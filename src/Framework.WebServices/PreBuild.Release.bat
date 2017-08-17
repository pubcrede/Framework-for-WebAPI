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

if "%Configuration%"=="" SET Configuration="Release"

REM \App_Data\*.mdf
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\*.mdf" -r
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\*.ldf" -r

REM \App_Data\AppSettings.config
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\AppSettings.config" -r
%WINDIR%\system32\xcopy.exe  "%FullPath%App_Data\AppSettings.%Configuration%.config" "%FullPath%App_Data\AppSettings.config" /f/r/c/y
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\AppSettings.config" +r

REM \App_Data\ConnectionStrings.config
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\ConnectionStrings.config" -r
%WINDIR%\system32\xcopy.exe  "%FullPath%App_Data\ConnectionStrings.%Configuration%.Config" "%FullPath%App_Data\ConnectionStrings.config" /f/r/c/y
%WINDIR%\system32\attrib.exe "%FullPath%App_Data\ConnectionStrings.config" +r

exit 0
