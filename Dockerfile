FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AA2ApiNET6.csproj", "./"]
RUN dotnet restore "./AA2ApiNET6.csproj"
COPY . .
RUN dotnet build "AA2ApiNET6.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AA2ApiNET6.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AA2ApiNET6.dll"]