FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.csproj .
RUN ls
RUN dotnet restore
RUN ls
COPY . .
RUN dotnet publish -c Release -o bin --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/bin .
ENTRYPOINT ["dotnet", "Service.dll"]
