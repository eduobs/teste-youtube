FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["teste-youtube/teste-youtube.csproj", "teste-youtube/"]
RUN dotnet restore "teste-youtube/teste-youtube.csproj"
COPY . .
WORKDIR "/src/teste-youtube"
RUN dotnet build "teste-youtube.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "teste-youtube.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "teste-youtube.dll"]