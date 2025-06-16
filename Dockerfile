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

# Install required packages
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    curl \
    libpq-dev \
    postgresql-client \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:10000
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Configure memory limits
ENV COMPlus_GCHeapHardLimit=0x10000000
ENV COMPlus_GCHeapHardLimitPercent=50

EXPOSE 10000

# Add healthcheck
HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
    CMD curl -f http://localhost:10000/ || exit 1

ENTRYPOINT ["dotnet", "DrsfanWebApp.dll"] 