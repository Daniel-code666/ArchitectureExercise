# etapa de compilación

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ArchitectureExercise/ArchitectureExercise.csproj", "ArchitectureExercise/"]
COPY ["Architecture.Domain/Architecture.Domain.csproj", "Architecture.Domain/"]
COPY ["Architecture.Application/Architecture.Application.csproj", "Architecture.Application/"]
COPY ["Architecture.Infrastructure/Architecture.Infrastructure.csproj", "Architecture.Infrastructure/"]

RUN dotnet restore "ArchitectureExercise/ArchitectureExercise.csproj"

COPY . .

WORKDIR "/src/ArchitectureExercise"
RUN dotnet publish "ArchitectureExercise.csproj" -c Release -o /app/publish /p:UseAppHost=false

# etapa de ejecución

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ArchitectureExercise.dll"]