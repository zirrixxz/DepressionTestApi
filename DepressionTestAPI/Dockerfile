FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8081

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DepressionTestAPI/DepressionTestAPI.csproj", "DepressionTestAPI/"]
RUN dotnet restore "DepressionTestAPI/DepressionTestAPI.csproj"
COPY . .
WORKDIR "/src/DepressionTestAPI"
RUN dotnet build "DepressionTestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DepressionTestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepressionTestAPI.dll"]
