FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# migration (not working)
RUN dotnet tool install --version 6.0.25 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations add InitialCreate
# RUN dotnet ef database update # done at run time
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "CharacterSheetCreatorBack.dll"]
