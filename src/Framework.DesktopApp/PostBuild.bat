ECHO OFF
REM Starting PostBuild.bat
REM Note: Beware that .bat files in VS have junk characters at beginning that must be removed via Binary Editor.
REM PostBuildEvent: Call $(ProjectDir)PostBuild.Bat $(TargetDir) $(TargetName)
REM Common are: $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug

REM Locals
SET LibFolder=\lib\GenesysFramework
SET FullPath=%1%2
SET FullPath=%FullPath:"=%
SET FullPath="%FullPath%"
SET RootNamespace=%4
SET RootNamespace=%RootNamespace:"=%


REM Copying project output to build location
Echo FullPath: %FullPath% 
Echo 3: %3
Echo RootNamespace: %RootNamespace%
Echo   to %LibFolder%

MD %LibFolder%
%WINDIR%\system32\attrib.exe %LibFolder%\*.* -r /s
%WINDIR%\system32\xcopy.exe %FullPath%.* %LibFolder%\*.* /f/s/e/r/c/y
%WINDIR%\system32\xcopy.exe %1Properties\*.rd.xml %LibFolder%\%RootNamespace%\Properties\*.* /s/r/y
%WINDIR%\system32\xcopy.exe %FullPath%*.png %LibFolder%\%RootNamespace%\*.* /s/r/y
%WINDIR%\system32\xcopy.exe %FullPath%*.xbf %LibFolder%\%RootNamespace%\*.* /s/r/y
%WINDIR%\system32\xcopy.exe %FullPath%*.xml %LibFolder%\%RootNamespace%\*.* /s/r/y
%WINDIR%\system32\xcopy.exe %3 %LibFolder%\*.* /s/r/y
%WINDIR%\system32\xcopy.exe %FullPath%*.pri %LibFolder%\*.* /s/r/y
exit 0