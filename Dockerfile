# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore NuGet packages
RUN dotnet restore EcommerceAutoPart/EcommerceAutoPart.csproj

# Build and publish to /app/publish
RUN dotnet publish EcommerceAutoPart/EcommerceAutoPart.csproj -c Release -o /app/publish

# Stage 2: Run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port 80 (required by Render)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "EcommerceAutoPart.dll"]
