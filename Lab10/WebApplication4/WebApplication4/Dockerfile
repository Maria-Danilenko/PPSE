#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENV ConnectionStrings__DefaultConnection="Server=DESKTOP-GF8REUK\\SQLEXPRESS;Database=Pizza;Trusted_Connection=true;Encrypt=False"
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
