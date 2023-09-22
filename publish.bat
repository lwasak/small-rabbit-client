@ECHO OFF

dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained -o publish

PAUSE
