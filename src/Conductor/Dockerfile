FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base

WORKDIR /app

COPY Conductor.Domain.Scripting .
COPY Conductor.Domain .
COPY Conductor.Steps .
COPY Conductor.Storage .
COPY Conductor .

ENTRYPOINT ["dotnet", "Conductor.dll"]