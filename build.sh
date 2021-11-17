git pull
cd ModelSaber.API
git pull
cd ../ModelSaber.Main
git pull
cd ..
dotnet build ./ModelSaber.API/ModelSaber.API.csproj -o ./Build -r linux-x64 -c Release
dotnet publish ./ModelSaber.API/ModelSaber.API.csproj -o ./Publish -r linux-x64 -c Release /p:PublishSingleFile=true
dotnet build ./ModelSaber.Main/ModelSaber.Main.csproj -o ./Build -r linux-x64 -c Release
dotnet publish ./ModelSaber.Main/ModelSaber.Main.csproj -o ./Publish -r linux-x64 -c Release /p:PublishSingleFile=true
systemctl restart modelsaberapi
systemctl restart modelsaber
