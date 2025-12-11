Real Estate API – Technical Test (Senior .NET Developer)

Este proyecto implementa una API para la gestión de propiedades inmobiliarias en Estados Unidos.
La solución fue desarrollada siguiendo buenas prácticas de arquitectura moderna en .NET, priorizando mantenibilidad, escalabilidad y separación de responsabilidades.

Tecnologías utilizadas

.NET 10.0.100
Entity Framework Core
SQL Server
MediatR (CQRS)
NUnit (Unit Tests)
FluentValidation
AutoMapper

Seguridad por defecto de ASP.NET Core Identity

Arquitectura

La solución está organizada siguiendo principios de Clean Architecture, con separación explícita entre capas:

src/
 ├── Domain
 ├── Application
 ├── Infrastructure
 └── WebApi

Domain

Contiene las entidades del negocio y reglas básicas.
No depende de ninguna otra capa.

Application

Contiene la lógica de aplicación usando:

CQRS con MediatR (Commands y Queries)

Validaciones con FluentValidation

Pipeline Behaviors (Validation, Authorization, Logging)

Mapeos a DTOs con AutoMapper

Infrastructure

Encapsula acceso a datos:

Entity Framework Core

Configuraciones de entidades (Fluent API)

Persistencia en SQL Server

Repositorios y servicios externos

WebApi

Expone los endpoints REST:

Versionado

Autenticación

Swagger

Manejo global de excepciones

Endpoints organizados por recursos

Features desarrolladas

La API implementa los servicios solicitados:

1. Crear Property Building

POST /api/propertybuildings

Valida campos requeridos

Inserta en base de datos

Genera un InternalCode secuencial usando SQL Server Sequence

Retorna el ID creado

2. Agregar imagen a la propiedad

POST /api/PropertyBuildings/{id}/addimage

Características:

Soporta PNG, JPEG

Validación de tamaño máximo (2mb)

Almacena en formato binario (varbinary(max))  //pudiera  varchar tradicionale y guardar un enlace a un servicio externo, sin embargo por efectos practicos se hizo de este modo.

Se guarda ContentType y Enabled

3. Cambiar precio

PATCH /api/propertybuildings/{id}/price

Actualiza el precio

Se deja un eventHandler dummy de trazabilidad

Valida que el nuevo valor sea mayor a 0 y que no sea igual al anterior

4. Actualizar propiedad

PUT /api/propertybuildings/{id}

Actualiza campos editables

Valida existencia del registro

Aplica reglas de negocio y validaciones

5. Listar propiedades con filtros

GET /api/PropertyBuildings/byfilters

Filtros disponibles:

Nombre
Rango de precio
Año de construcción
Código interno
Dirección 
Propietario (solo aplica el filtro si usas el token de autorización)
Paginación

La query usa:

Expresiones dinámicas
Proyección eficiente con AutoMapper
Ejecución optimizada SQL



Pruebas Automatizadas (NUnit)

Incluye pruebas para:

Commands

CreatePropertyBuilding

AddPropertyBuildingImage

ChangePropertyBuildingPrice

UpdatePropertyBuilding

Queries

GetPropertyBuildingsByFilter

Validaciones

Campos requeridos
Tamaño de archivo
Tipos archivo permitidos
Rango de precio
existencia de entidades

Pruebas funcionales

Inserción real en SQL
Validación de secuencia (NEXT VALUE FOR)
Inserción de imágenes binaras
Base de datos

Ejecución del Proyecto

Configurar cadena de conexión en appsettings.Development.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=RealEstateDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Levantar la API:

cd src/WebApi
dotnet run


Ir al Swagger:

https://localhost:5001/swagger

Para cualquier soporte adicional, por favor contactarme.
