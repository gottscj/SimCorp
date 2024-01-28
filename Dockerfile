FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env


COPY SimCorp.Api/ /src/

RUN dotnet restore /src/SimCorp.Api.csproj
RUN dotnet publish /src/SimCorp.Api.csproj -c Release -o /publish /p:UseAppHost=false

EXPOSE 8080
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "SimCorp.Api.dll"]
