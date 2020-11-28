FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY ./bin/Release/netcoreapp3.1/ubuntu.18.04-x64/publish ./
ENTRYPOINT ["dotnet", "UG.WebApi.dll"]