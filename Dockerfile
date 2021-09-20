# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY drawgo_test/*.csproj ./drawgo_test/
RUN dotnet restore

# copy everything else and build app
COPY drawgo_test/. ./drawgo_test/
WORKDIR /source/drawgo_test
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
# ENTRYPOINT ["dotnet", "drawgo_test.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet drawgo_test.dll