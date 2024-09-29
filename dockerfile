FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
EXPOSE 8000

COPY ./Backend/TaxAssistant/ .
RUN dotnet restore --disable-parallel

RUN dotnet publish -c release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /publish ./

ENV ASPNETCORE_URLS http://*:8000

ENTRYPOINT ["dotnet", "TaxAssistant.dll"]