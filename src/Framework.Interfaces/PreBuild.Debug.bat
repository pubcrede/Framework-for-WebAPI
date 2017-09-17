ECHO Starting PreBuild.bat
REM Usage: Call "$(MSBuildProjectDirectory)\PreBuild.$(ConfigurationName).bat" "$(MSBuildProjectDirectory)" "$(ConfigurationName)"
REM Vars:  $(ProjectName) = MyCo.Framework. Models, $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug, $(ConfigurationName) = "Debug"

REM Locals
SET FullPath=%1
SET FullPath=%FullPath:"=%
SET ProductFolder="framework.interfaces"

ECHO ** PreBuild.bat **
ECHO FullPath: %FullPath%
SET Configuration=%2
ECHO Configuration: %Configuration%



exit 0
