FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY CustomerService.sln ./
COPY /src/Customer.API/Customer.API.csproj ./Customer.API/
COPY /src/Customer.Service/Customer.Service.csproj ./Customer.Service/
COPY /src/Customer.Data/Customer.Data.csproj ./Customer.Data/
COPY /src/Customer.Domain/Customer.Domain.csproj ./Customer.Domain/

RUN dotnet restore
COPY . .

WORKDIR "/src/Customer.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.API.dll"]