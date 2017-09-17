ECHO OFF
REM Starting PostBuild.bat
REM Note: Beware that .bat files in VS have junk characters at beginning that must be removed via Binary Editor.
REM Usage: Call $(ProjectDir)PostBuild.Bat $(TargetDir) $(TargetName)
REM Common are: $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug

REM Locals
SET LibFolder=\lib\Genesys-Framework
SET FullPath=%1
SET FullPath=%FullPath:"=%


REM Copying project output to build location
Echo Input: %FullPath% to %LibFolder%

MD %LibFolder%
%WINDIR%\system32\attrib.exe %LibFolder%\*.* -r /s
%WINDIR%\system32\xcopy.exe "%FullPath%\*.*" "%LibFolder%\*.*" /d/f/s/e/r/c/y
exit 0