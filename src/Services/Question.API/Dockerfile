#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Question.API/Question.API.csproj", "Services/Question.API/"]
COPY ["Shared/Question.Core/Question.Core.csproj", "Shared/Question.Core/"]
RUN dotnet restore "Services/Question.API/Question.API.csproj"
COPY . .
WORKDIR "/src/Services/Question.API"
RUN dotnet build "Question.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Question.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Question.API.dll"]