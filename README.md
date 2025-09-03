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
   **Tener instalado docker.

2. **Clonar el repositorio:**

    -Ejecutar este comando desde la carpeta raíz principal del repositorio:
      Ejemplo: E:\RealEstate.PropertyManager

    -Comando:
      docker compose up -d --build



