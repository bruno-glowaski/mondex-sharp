FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY MonDexSharp.Worker.sln .
COPY ./Core/*.csproj ./Core/
COPY ./Data/*.csproj ./Data/
COPY ./Jobs/Healthcheck/*.csproj ./Jobs/Healthcheck/
COPY ./Jobs/PokeApiImporter/*.csproj ./Jobs/PokeApiImporter/
COPY ./Worker/*.csproj ./Worker/
RUN dotnet restore

COPY ./Core/. ./Core/
COPY ./Data/. ./Data/
COPY ./Jobs/. ./Jobs/
COPY ./Worker/. ./Worker/
WORKDIR /source/Worker
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MonDexSharp.Worker.dll"]
