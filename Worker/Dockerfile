FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY *.sln .
COPY ./Core/*.csproj ./Core/
COPY ./Worker/*.csproj ./Worker/
COPY ./Jobs/Healthcheck/*.csproj ./Jobs/Healthcheck/
RUN dotnet sln remove \
  Core.Tests/MonDexSharp.Core.Tests.csproj \
  Backend/MonDexSharp.Backend.csproj
RUN dotnet restore

COPY ./Core/. ./Core/
COPY ./Worker/. ./Worker/
COPY ./Jobs/. ./Jobs/
WORKDIR /source/Worker
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MonDexSharp.Worker.dll"]
