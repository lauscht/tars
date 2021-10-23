FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Framework"]
RUN dotnet restore "/src/Kipp.Framework/Kipp.Framework.csproj"
RUN dotnet build "Kipp.Framework/Kipp.Framework.csproj" -c Release -o /app/build
 
COPY ["Kipp.Server", "Kipp.Server"]
RUN dotnet restore "/src/Kipp.Server/Kipp.Server.csproj"
RUN dotnet build "Kipp.Server/Kipp.Server.csproj" -c Release -o /app/build
 
FROM build as test
COPY ["Kipp.Server.Tests", "Kipp.Server.Tests"]
RUN dotnet test "/src/Kipp.Server.Tests/Kipp.Server.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"

FROM build AS publish
RUN dotnet publish "Kipp.Server/Kipp.Server.csproj" -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG UID=1000
ARG GID=1000
RUN chown -R ${UID} /app
USER ${UID}

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Kipp.Server.dll"]