FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BookSearch.csproj", "."]
RUN dotnet restore "BookSearch.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BookSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookSearch.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookSearch.dll"]
