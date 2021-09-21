FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ./ ./src
WORKDIR /src
RUN dotnet restore "./drawgo.csproj"
RUN dotnet build --no-restore "./drawgo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "./drawgo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
# ENTRYPOINT ["dotnet", "drawgo.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet drawgo.dll

# FROM mysql
# ENV MYSQL_ROOT_PASSWORD password
# ENV MYSQL_DATABASE drawgo