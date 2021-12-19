FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SmartLockDemo.WebAPI/SmartLockDemo.WebAPI.csproj", "SmartLockDemo.WebAPI/"]
COPY ["SmartLockDemo.Business/SmartLockDemo.Business.csproj", "SmartLockDemo.Business/"]
COPY ["SmartLockDemo.Infrastructure/SmartLockDemo.Infrastructure.csproj", "SmartLockDemo.Infrastructure/"]
COPY ["SmartLockDemo.Data/SmartLockDemo.Data.csproj", "SmartLockDemo.Data/"]
RUN dotnet restore "SmartLockDemo.WebAPI/SmartLockDemo.WebAPI.csproj"
COPY . .
WORKDIR "/src/SmartLockDemo.WebAPI"
RUN dotnet build "SmartLockDemo.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartLockDemo.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartLockDemo.WebAPI.dll"]