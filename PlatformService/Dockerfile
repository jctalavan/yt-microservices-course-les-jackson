# Usar una imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar los archivos de proyecto y restaurar dependencias
COPY ./*.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Usar una imagen base de .NET Runtime para correr la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exponer el puerto que usa la aplicación
EXPOSE 5037

# Definir el punto de entrada para correr la aplicación
ENTRYPOINT ["dotnet", "PlatformService.dll"]

# docker build -t platformservice-api:latest .
# docker run -d -p 5037:8080 --name platformservice-api-container platformservice-api

# Mapea el puerto 8080 del contenedor al puerto 5037 de la máquina local