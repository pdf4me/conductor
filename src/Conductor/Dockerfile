FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Conductor.Domain.Scripting/Conductor.Domain.Scripting.csproj", "src/Conductor.Domain.Scripting/"]
COPY ["src/Conductor.Domain/Conductor.Domain.csproj", "src/Conductor.Domain/"]
COPY ["src/Conductor.Steps/Conductor.Steps.csproj", "src/Conductor.Steps/"]
COPY ["src/Conductor.Storage/Conductor.Storage.csproj", "src/Conductor.Storage/"]
COPY ["src/Conductor/Conductor.csproj", "src/Conductor/"]

RUN dotnet restore "src/Conductor/Conductor.csproj"
COPY . .
WORKDIR "/src/Conductor"
RUN dotnet build "Conductor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Conductor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Conductor.dll"]