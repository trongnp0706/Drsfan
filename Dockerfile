# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Drsfan.sln", "./"]
COPY ["DrsfanWebApp/DrsfanWebApp.csproj", "DrsfanWebApp/"]
COPY ["Drsfan.DataAccess/Drsfan.DataAccess.csproj", "Drsfan.DataAccess/"]
COPY ["Drsfan.Models/Drsfan.Models.csproj", "Drsfan.Models/"]
COPY ["Drsfan.Utility/Drsfan.Utility.csproj", "Drsfan.Utility/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish
RUN dotnet build "DrsfanWebApp/DrsfanWebApp.csproj" -c Release -o /app/build
RUN dotnet publish "DrsfanWebApp/DrsfanWebApp.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "DrsfanWebApp.dll"] 