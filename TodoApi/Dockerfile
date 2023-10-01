# Stage 1: Build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

WORKDIR /src

# Copy the project file
COPY ./TodoApi/TodoApi.csproj ./TodoApi/

# Restore NuGet packages
RUN dotnet restore ./TodoApi/TodoApi.csproj

# Copy the application files
COPY ./TodoApi/ ./TodoApi/

# Publish the application
RUN dotnet publish ./TodoApi/TodoApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app

# Copy the published application from the previous stage
COPY --from=publish /app/publish .

# Expose port 8080 and set the ASP.NET Core URLs
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "TodoApi.dll"]
