FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ./drawgo_test ./src/drawgo_test
WORKDIR /src
RUN dotnet restore "./drawgo_test/drawgo_test.csproj"
RUN dotnet build --no-restore "./drawgo_test/drawgo_test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "./drawgo_test/drawgo_test.csproj" -c Release -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
# ENTRYPOINT ["dotnet", "drawgo_test.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet drawgo_test.dll