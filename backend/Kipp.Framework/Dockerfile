FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Kipp.Framework", "Kipp.Framework"]
RUN dotnet restore "/src/Kipp.Framework/Kipp.Framework.csproj"
RUN dotnet build "Kipp.Framework/Kipp.Framework.csproj" -c Release -o /app/build
 
FROM build as test
COPY ["Kipp.Framework.Tests", "Kipp.Framework.Tests"]
RUN dotnet test "/src/Kipp.Framework.Tests/Kipp.Framework.Tests.csproj" --collect:"XPlat Code Coverage" -r "/src/TestResults/"
