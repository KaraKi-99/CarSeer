# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
 #// ["CarSeer/CarSeer.csproj", "CarSeer/"] --disable-parallel

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . . 
RUN dotnet restore "./CarSeer/CarSeer.csproj" --disable-parallel
RUN dotnet publish "./CarSeer/CarSeer.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/ ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "CarSeer.dll"]
