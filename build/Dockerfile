# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY src/Acme.Orders.Api/bin/Release/netcoreapp3.1/publish/. .
ENTRYPOINT dotnet Acme.Orders.Api.dll