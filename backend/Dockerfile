FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Framework"]
COPY ["Kipp.Server", "Kipp.Server"]
RUN dotnet restore "/src/Kipp.Server/Kipp.Server.csproj"
RUN dotnet build "Kipp.Server/Kipp.Server.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Kipp.Server/Kipp.Server.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Kipp.Server.dll"]

FROM build as test
COPY ["Kipp.Framework.Tests", "Kipp.Framework.Tests"]
COPY ["Kipp.Server.Tests", "Kipp.Server.Tests"]
RUN dotnet test "/src/Kipp.Framework.Tests/Kipp.Framework.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"
RUN dotnet test "/src/Kipp.Server.Tests/Kipp.Server.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"
