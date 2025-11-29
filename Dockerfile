# U¿ycie oficjalnego obrazu .NET 8.0 SDK do budowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiowanie plików projektu i przywracanie zale¿noœci
COPY CoachBuddy.sln ./
COPY CoachBuddy.Application/*.csproj CoachBuddy.Application/
COPY CoachBuddy.Infrastructure/*.csproj CoachBuddy.Infrastructure/
COPY CoachBuddy.Domain/*.csproj CoachBuddy.Domain/
COPY CoachBuddy/*.csproj CoachBuddy/

COPY CoachBuddy.ApplicationTests/*.csproj CoachBuddy.ApplicationTests/
COPY CoachBuddy.DomainTests/*.csproj CoachBuddy.DomainTests/
COPY CoachBuddy.MVCTests/*.csproj CoachBuddy.MVCTests/

RUN dotnet restore CoachBuddy/CoachBuddy.MVC.csproj

# Kopiowanie wszystkich plików Ÿród³owych i kompilacja
COPY . ./
WORKDIR /app/CoachBuddy
RUN dotnet publish -c Release -o /out

# U¿ycie oficjalnego obrazu runtime do uruchamiania aplikacji
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y \
    krb5-user \
    libkrb5-dev \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

COPY --from=build /out ./

# Konfiguracja aplikacji
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "CoachBuddy.MVC.dll"]
