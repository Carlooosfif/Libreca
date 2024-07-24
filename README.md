# Libreca

Libreca es una aplicación web para la gestión de préstamos y devoluciones de libros. El proyecto incluye una aplicación principal y un microservicio para la búsqueda de libros, utilizando RabbitMQ para la mensajería y notificaciones.

## Características

- Gestión de usuarios con registro y autenticación.
- Préstamo y devolución de libros.
- Búsqueda de libros mediante un microservicio.
- Notificaciones por correo electrónico usando RabbitMQ.

## Tecnologías Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- RabbitMQ
- Docker
- SQL Server

## Requisitos Previos

- Docker
- .NET 8.0 SDK
- SQL Server

## Configuración del Proyecto

1. **Clonar el Repositorio**

  - git clone https://github.com/tu_usuario/Libreca.git
  - cd Libreca
   
## Configuración de la Base de Datos

Asegúrate de tener una instancia de SQL Server en funcionamiento y actualiza la cadena de conexión en `appsettings.json` según tu configuración.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

```

## Construir y Ejecutar los Contenedores Docker
- Navega hasta el directorio del microservicio BookSearch y construye la imagen de Docker.

```
cd BookSearch
docker build -t booksearch-app .
docker run -d --name booksearch-container -p 8082:80 booksearch-app
```
-Levanta también RabbitMQ usando Docker.
```
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
## Ejecutar la Aplicación Principal
- Vuelve al directorio raíz del proyecto y ejecuta la aplicación principal.
```
cd ..
dotnet run --project Libreca
```
## Acceder a la Aplicación
- La aplicación principal estará disponible en https://localhost:7005/.

## Estructura del Proyecto
- Libreca/: Contiene la aplicación principal.
- Controllers/: Controladores de la aplicación.
- Models/: Modelos de datos.
- Views/: Vistas de la aplicación.
- wwwroot/: Archivos estáticos.
- BookSearch/: Contiene el microservicio de búsqueda de libros.
- Controllers/: Controladores del microservicio.
- Models/: Modelos de datos.
- Services/: Servicios de lógica del microservicio.
## Contribuciones
- Las contribuciones son bienvenidas. Por favor, envía un pull request o abre un issue para discutir los cambios que te gustaría realizar.
