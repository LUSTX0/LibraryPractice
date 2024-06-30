# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Checkout the repository with full history
# RUN git clone --depth=full <https://github.com/LUSTX0/LibraryPractice.git> .

# Install Nerdbank.GitVersioning
# RUN dotnet add <project>.csproj package Nerdbank.GitVersioning

# Copy the solution file and restore dependencies
COPY *.sln ./
COPY LibraryPractice/LIBRARY2.csproj LibraryPractice/
COPY Logic/Logic.csproj Logic/
COPY SQLcon/SQLcon.csproj SQLcon/



RUN dotnet restore

# Copy the remaining files and build the application

 COPY . .

WORKDIR /src/LibraryPractice
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LIBRARY2.dll"]