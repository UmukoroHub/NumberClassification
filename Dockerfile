# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.sln .
COPY NumberClassification/NumberClassification.csproj NumberClassification/
RUN dotnet restore NumberClassification/NumberClassification.csproj

# Copy the rest of the application and build it
COPY NumberClassification/. NumberClassification/
WORKDIR /app/NumberClassification
RUN dotnet publish -c Release -o out

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/NumberClassification/out ./
ENTRYPOINT ["dotnet", "NumberClassification.dll"]
