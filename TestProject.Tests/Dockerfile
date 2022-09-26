#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.0 AS build
WORKDIR /src
COPY ["TestProject.Tests/TestProject.Tests.csproj", "TestProject.Tests/"]
COPY ["TestProject.WebAPI/TestProject.WebAPI.csproj", "TestProject.WebAPI/"]
RUN dotnet restore "TestProject.Tests/TestProject.Tests.csproj"
COPY . .
WORKDIR "/src/TestProject.Tests"
RUN dotnet build "TestProject.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestProject.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProject.Tests.dll"]