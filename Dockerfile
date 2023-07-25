FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the csproj file and restore the project dependencies 
COPY ./ ./
RUN dotnet restore "src/Tickets.Api/Tickets.Api.csproj" --no-cache

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish "src/Tickets.Api/Tickets.Api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Tickets.Api.dll"]