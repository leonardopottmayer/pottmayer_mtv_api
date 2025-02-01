# Uses a base SDK .NET image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app

# Copies the project files and restores dependencies
COPY . .
RUN dotnet restore

# Publishes the application
RUN dotnet publish -o /app/published-app

# Uses the base ASP.NET Core image to execute the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENV ASPNETCORE_URLS=http://+:5035

ENTRYPOINT [ "dotnet", "/app/Gowther.Api.dll" ]
