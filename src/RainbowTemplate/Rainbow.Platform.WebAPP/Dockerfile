#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Rainbow.Platform.WebAPP/Rainbow.Platform.WebAPP.csproj", "Rainbow.Platform.WebAPP/"]
COPY ["Rainbow.Data/Rainbow.Data.csproj", "Rainbow.Data/"]
COPY ["Rainbow.Platform.Authorize/Rainbow.Platform.Authorize.csproj", "Rainbow.Platform.Authorize/"]
COPY ["Rainbow.Services.Abstractions/Rainbow.Services.Abstractions.csproj", "Rainbow.Services.Abstractions/"]
COPY ["Rainbow.ViewModels/Rainbow.ViewModels.csproj", "Rainbow.ViewModels/"]
COPY ["Rainbow.Common/Rainbow.Common.csproj", "Rainbow.Common/"]
COPY ["Rainbow.Platform.Controllers/Rainbow.Platform.Controllers.csproj", "Rainbow.Platform.Controllers/"]
COPY ["Rainbow.Services/Rainbow.Services.csproj", "Rainbow.Services/"]
COPY ["Rainbow.Events/Rainbow.Events.csproj", "Rainbow.Events/"]
COPY ["Rainbow.Models/Rainbow.Models.csproj", "Rainbow.Models/"]
RUN dotnet restore "Rainbow.Platform.WebAPP/Rainbow.Platform.WebAPP.csproj"
COPY . .
WORKDIR "/src/Rainbow.Platform.WebAPP"
RUN dotnet build "Rainbow.Platform.WebAPP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rainbow.Platform.WebAPP.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rainbow.Platform.WebAPP.dll"]