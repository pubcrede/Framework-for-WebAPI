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

REM Pre-Build logic here

Exit 0
