# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY . .
ENTRYPOINT dotnet /app/Acme.Orders.Api.dll