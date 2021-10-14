FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Framework"]
RUN dotnet restore "/src/Kipp.Framework/Kipp.Framework.csproj"
RUN dotnet build "Kipp.Framework/Kipp.Framework.csproj" -c Release -o /app/build
 
COPY ["Kipp.Identity", "Kipp.Identity"]
RUN dotnet restore "/src/Kipp.Identity/Kipp.Identity.csproj"
RUN dotnet build "Kipp.Identity/Kipp.Identity.csproj" -c Release -o /app/build
 
FROM build as test
COPY ["Kipp.Identity.Tests", "Kipp.Identity.Tests"]
RUN dotnet test "/src/Kipp.Identity.Tests/Kipp.Identity.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"

FROM build AS publish
RUN dotnet publish "Kipp.Identity/Kipp.Identity.csproj" -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Kipp.Identity.dll"]