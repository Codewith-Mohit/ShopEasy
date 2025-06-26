# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything to /src
COPY . .

# Restore from .csproj file
RUN dotnet restore "ShopEasy.API/ShopEasy.API.csproj"

# Build and publish to a temporary output folder
RUN dotnet publish "ShopEasy.API/ShopEasy.API.csproj" -c Release -o /app/build

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy built app from previous stage
COPY --from=build /app/build .

# Set entry point
ENTRYPOINT ["dotnet", "ShopEasy.API.dll"]
