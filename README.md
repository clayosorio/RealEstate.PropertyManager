# PropertyManager

## Descripción
**PropertyManager** es una solución desarrollada en .NET 9 y C# 13.0 para la gestión de propiedades inmobiliarias, propietarios y autenticación de usuarios. El proyecto está estructurado en capas, siguiendo buenas prácticas de arquitectura y diseño, y expone una API RESTful con endpoints para operaciones CRUD, autenticación y manejo de imágenes de propiedades.

## Características principales
- **Gestión de propiedades:** Alta, modificación, listado con filtros y carga de imágenes.
- **Gestión de propietarios:** Registro y administración de propietarios.
- **Autenticación:** Endpoint para inicio de sesión.
- **Manejo de errores global:** Middleware para captura y respuesta uniforme de excepciones.
- **Documentación de endpoints:** Archivos dedicados para la documentación de cada endpoint.
- **Inyección de dependencias:** Configuración centralizada y extensible.
- **Permisos y autorización:** Decoradores para proteger rutas según permisos.
- **Extensiones para configuración:** Métodos para agregar endpoints, CORS, monitoreo y servicios.

## Estructura del proyecto
- **src/PropertyManager.Api/**: Proyecto principal de la API.
  - **Endpoints/**: Implementaciones de los endpoints REST.
  - **Extensions/**: Métodos de extensión para configuración y servicios.
  - **Infrastructure/**: Componentes transversales como el manejador global de excepciones.
  - **DependencyInjection.cs**: Configuración de servicios y dependencias.
  - **appsettings.json**: Configuración de la aplicación.
- **src/PropertyManager.Application/**: Lógica de negocio y casos de uso.
- **src/PropertyManager.Domain/**: Entidades, repositorios y errores de dominio.
- **PropertyManager.Test/**: Pruebas unitarias con NUnit y Moq.

## Instalación y ejecución
1. **Requisitos previos:**
   - .NET 9 SDK
   - Visual Studio 2022 o superior

2. **Clonar el repositorio:**


3. **Restaurar paquetes:**
4. **Compilar la solución:**
5. **Ejecutar la API:**

## Extensibilidad
El proyecto utiliza métodos de extensión para agregar endpoints, servicios y middlewares de forma sencilla y escalable. Los endpoints se registran automáticamente mediante reflexión y se agrupan por permisos cuando es necesario.

## Configuración
La configuración de la aplicación se encuentra en `appsettings.json`, donde se pueden ajustar parámetros como cadenas de conexión, políticas de CORS, y otros valores relevantes.

## Dependencias externas

Este proyecto requiere los siguientes servicios externos, que se pueden levantar fácilmente con Docker:

### SQL Server


## Servicios externos con Docker

Para el funcionamiento local del proyecto, necesitas levantar los siguientes contenedores Docker:

### SQL Server

Ejecuta el siguiente comando para iniciar SQL Server 2022 en Docker:

docker compose up -d --build



