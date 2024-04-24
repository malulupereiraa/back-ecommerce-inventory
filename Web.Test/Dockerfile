## Build Stage
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
#WORKDIR /src
#COPY ["Web.Test/Web.Test.csproj", "Web.Test/"]
##COPY . . 
#RUN dotnet restore "./Web.Test/Web.Test.csproj"
#RUN dotnet publish "./Web.Test/Web.Test.API.csproj" -c release -o /app --no-restore
#
## Serve Stage
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#COPY --from=build /app ./
#
#EXPOSE 5000
#
#ENTRYPOINT ["dotnet", "Web.Test.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["Web.Test/Web.Test.csproj", "Web.Test/"]
RUN dotnet restore "./Web.Test/Web.Test.csproj"
COPY . .
WORKDIR "/src/Web.Test"
RUN dotnet build "./Web.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ENV ASPNETCORE_ENVIRONMENT=Development
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "Web.Test.dll"]