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

# Para construir la imagen a partir de un fichero Dockerfile utilizamos el siguiente comando:
# docker build -t platformservice-api:latest .
# -t Etiqueta la imagen con un nombre (y opcionalemente con un tag)
# El . al final indica el contexto de construcción. En este caso el . indica que el contexto de construcción es el directorio actual. Docker buscará un fichero 
# Dockerfile en este directorio para obtener las instrucciones de construcción de la imagen.

# Para construir un contenedor corriendo una determinada imagen utilizamos el siguiente comando:
# docker run -d -p 5037:8080 --name platformservice-api-container platformservice-api
# -d Ejecuta el contenedor en segundo plano
# -p Mapea el puerto 8080 del contenedor (puerto por defecto en aplicaciones .net) al puerto 5037 de la máquina local (podemos usar otro si queremos)
# --name Nombre que se le pone al contenedor
# <nombre de la imagen que se ejecuta en el contenedor que vamos a crear> (platformservice-api)

# Para publicar la imagen en Docker hub
# docker login
# docker tag imagen_local docker_hub_user/docker_hub_repo:tagname
# docker push docker_hub_user/docker_hub_repo:tagname