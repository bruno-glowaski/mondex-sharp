FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY *.sln .
COPY ./Core/*.csproj ./Core/
COPY ./Backend/*.csproj ./Backend/
RUN dotnet sln remove \
  Core.Tests/MonDexSharp.Core.Tests.csproj \
  Worker/MonDexSharp.Worker.csproj \
  Jobs \
  Jobs/Healthcheck/MonDexSharp.Jobs.Healthcheck.csproj
RUN dotnet restore

COPY ./Core/. ./Core/
COPY ./Backend/. ./Backend/
WORKDIR /source/Backend
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MonDexSharp.Backend.dll"]
