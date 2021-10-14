FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Framework"]
RUN dotnet restore "/src/Kipp.Framework/Kipp.Framework.csproj"
RUN dotnet build "Kipp.Framework/Kipp.Framework.csproj" -c Release -o /app/build
 
COPY ["Kipp.Indentity", "Kipp.Indentity"]
RUN dotnet restore "/src/Kipp.Indentity/Kipp.Indentity.csproj"
RUN dotnet build "Kipp.Indentity/Kipp.Indentity.csproj" -c Release -o /app/build
 
FROM build as test
COPY ["Kipp.Indentity.Tests", "Kipp.Indentity.Tests"]
RUN dotnet test "/src/Kipp.Indentity.Tests/Kipp.Indentity.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"

FROM build AS publish
RUN dotnet publish "Kipp.Indentity/Kipp.Indentity.csproj" -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Kipp.Indentity.dll"]