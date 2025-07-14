@echo off
echo Building StartTurista.exe...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=false -p:PublishTrimmed=false -o ./publish
echo Build complete! StartTurista.exe is in the publish folder.
pause