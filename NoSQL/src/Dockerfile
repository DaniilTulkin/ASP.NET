#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pcf.Preference/Pcf.Preference.WebHost.csproj", "Pcf.Preference/"]
COPY ["Pcf.Preference.DataAccess/Pcf.Preference.DataAccess.csproj", "Pcf.Preference.DataAccess/"]
COPY ["Pcf.Preference.Core/Pcf.Preference.Core.csproj", "Pcf.PreferenceCore/"]
RUN dotnet restore "./Pcf.Preference/Pcf.Preference.WebHost.csproj"
COPY . .
WORKDIR "/src/Pcf.Preference"
RUN dotnet build "./Pcf.Preference.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Pcf.Preference.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcf.Preference.WebHost.dll"]