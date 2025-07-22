# Use official ASP.NET Core image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all solution files
COPY . .

# Restore and build
RUN dotnet restore EcommerceAutoPart/EcommerceAutoPart.csproj
RUN dotnet publish EcommerceAutoPart/EcommerceAutoPart.csproj -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EcommerceAutoPart.dll"]
