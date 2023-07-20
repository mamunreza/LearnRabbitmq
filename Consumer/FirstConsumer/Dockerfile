FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS frmqapbuild
WORKDIR /src
COPY Consumer/FirstConsumer/ .
COPY ../../NuGet.Config .

RUN dotnet restore --configfile ./NuGet.Config \
    && dotnet build --configuration Release --no-restore \
    && dotnet publish --configuration Release --self-contained=false --runtime linux-x64 --output output

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=frmqapbuild /src/output/ .
ENTRYPOINT ["dotnet", "FirstConsumer.dll"]