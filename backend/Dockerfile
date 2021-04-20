FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Server"]
RUN dotnet restore "Kipp.*/*.csproj"
WORKDIR "/src/"
RUN dotnet build "*.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Kipp.Server/Kipp.Server.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Kipp.Server.dll"]

FROM build as test
COPY ["Kipp.Framework.Tests", "Kipp.Server.Tests"]
RUN dotnet test "*.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"