# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy solution and project files to restore dependencies
COPY *.sln ./
COPY Bloggy.Api/*.csproj ./Bloggy.Api/
COPY Bloggy.Application/*.csproj ./Bloggy.Application/
COPY Bloggy.Domain/*.csproj ./Bloggy.Domain/
COPY Bloggy.Infrastructure/*.csproj ./Bloggy.Infrastructure/

# Restore the dependencies
RUN dotnet restore

# Copy the entire application source code into the container
COPY Bloggy.Api/ ./Bloggy.Api/
COPY Bloggy.Application/ ./Bloggy.Application/
COPY Bloggy.Domain/ ./Bloggy.Domain/
COPY Bloggy.Infrastructure/ ./Bloggy.Infrastructure/

# Publish the application in release mode
RUN dotnet publish Bloggy.Api/Bloggy.Api.csproj -c Release -o /app/publish

# Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port your backend will listen on
EXPOSE 5010

# Set the entry point to the application
ENTRYPOINT ["dotnet", "Bloggy.Api.dll"]
