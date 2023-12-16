FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env


COPY DanskeBank.Api/ /src/

RUN dotnet restore /src/DanskeBank.Api.csproj
RUN dotnet publish /src/DanskeBank.Api.csproj -c Release -o /publish /p:UseAppHost=false

EXPOSE 8080
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "DanskeBank.Api.dll", "--launch-profile", "Docker"]
