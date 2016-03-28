
rm.exe -rf Temp
mkdir Temp
rm.exe -rf Build
mkdir Build


mkdir Temp\tools

copy "%CD%\EncryptTool\bin\Release\EncryptTool.exe" "%CD%\Temp\tools\EncryptTool.exe" /Y
copy "%CD%\EncryptTool.nuspec" "%CD%\Temp\EncryptTool.nuspec" /Y
cd Temp
nuget pack "%CD%\EncryptTool.nuspec" -OutputDirectory "%CD%\..\Build"
cd ..

