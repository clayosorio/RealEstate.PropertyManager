#PropertyManager

## Descripción
**PropertyManager** es una solución desarrollada en **.NET 9** y **C# 13.0** para la gestión de propiedades inmobiliarias, propietarios y autenticación de usuarios.  

El proyecto está diseñado siguiendo principios de **Clean Architecture** y **buenas prácticas de diseño de software**, exponiendo una **API RESTful** con endpoints para operaciones CRUD, autenticación y manejo de imágenes.

---

##  Características principales
-  **Gestión de propiedades**: Alta, modificación, listado con filtros y carga de imágenes.  
- **Gestión de propietarios**: Registro y administración de propietarios.  
- **Autenticación**: Endpoint para inicio de sesión.  
- **Manejo de errores global**: Middleware centralizado para capturar y responder excepciones de forma uniforme.  
- **Documentación de endpoints**: Archivos dedicados para cada recurso.  
- **Inyección de dependencias**: Configuración centralizada y extensible.  
- **Permisos y autorización**: Decoradores para proteger rutas según permisos.  
- **Extensiones para configuración**: Métodos para agregar endpoints, CORS, monitoreo y servicios.  

---

## Estructura del proyecto
```bash
src/
├─ PropertyManager.Api/ # Proyecto principal de la API
│ ├─ Endpoints/ # Implementaciones de endpoints REST
│ ├─ Extensions/ # Métodos de extensión para configuración y servicios
│ ├─ Infrastructure/ # Middleware global y componentes transversales
│ ├─ DependencyInjection.cs # Configuración centralizada de servicios y dependencias
│ ├─ appsettings.json # Configuración de la aplicación
│
├─ PropertyManager.Application/ # Casos de uso y lógica de negocio
├─ PropertyManager.Domain/ # Entidades, repositorios y reglas de dominio
└─ PropertyManager.Test/ # Pruebas unitarias (xUnit / NUnit + Moq)
```

---


---

## Instalación y ejecución

# 1. Instalar Docker
```
https://docs.docker.com/engine/install/
```

# 2. Clonar el repositorio
```bash
git clone https://github.com/clayosorio/RealEstate.PropertyManager.git
```

# 3. Acceder a la carpeta raíz del proyecto
```
cd PropertyManager
```

# 4. Construir y levantar los contenedores con Docker
```
docker compose up -d --build
```
