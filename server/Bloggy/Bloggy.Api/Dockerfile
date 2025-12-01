# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) to the container
COPY ./Bloggy/Bloggy.Api/*.csproj ./

# Restore the dependencies
RUN dotnet restore

# Copy the entire application source code into the container
COPY ./Bloggy/Bloggy.Api/ ./

# Publish the application in release mode
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port your backend will listen on
EXPOSE 5010

# Set the entry point to the application
ENTRYPOINT ["dotnet", "Bloggy.Api.dll"]
