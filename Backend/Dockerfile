FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY MonDexSharp.Backend.sln .
COPY ./Core/*.csproj ./Core/
COPY ./Data/*.csproj ./Data/
COPY ./Backend/*.csproj ./Backend/
RUN dotnet restore

COPY ./Core/. ./Core/
COPY ./Data/. ./Data/
COPY ./Backend/. ./Backend/
WORKDIR /source/Backend
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MonDexSharp.Backend.dll"]
