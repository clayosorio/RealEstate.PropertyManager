# 🏠 PropertyManager

## 📖 Descripción
**PropertyManager** es una solución desarrollada en **.NET 9** y **C# 13.0** para la gestión de propiedades inmobiliarias, propietarios y autenticación de usuarios.  

El proyecto está diseñado siguiendo principios de **Clean Architecture** y **buenas prácticas de diseño de software**, exponiendo una **API RESTful** con endpoints para operaciones CRUD, autenticación y manejo de imágenes.

---

## ✨ Características principales
- 📌 **Gestión de propiedades**: Alta, modificación, listado con filtros y carga de imágenes.  
- 👤 **Gestión de propietarios**: Registro y administración de propietarios.  
- 🔐 **Autenticación**: Endpoint para inicio de sesión.  
- ⚡ **Manejo de errores global**: Middleware centralizado para capturar y responder excepciones de forma uniforme.  
- 📑 **Documentación de endpoints**: Archivos dedicados para cada recurso.  
- 🛠️ **Inyección de dependencias**: Configuración centralizada y extensible.  
- 🔒 **Permisos y autorización**: Decoradores para proteger rutas según permisos.  
- 🧩 **Extensiones para configuración**: Métodos para agregar endpoints, CORS, monitoreo y servicios.  

---

## 🗂️ Estructura del proyecto
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

css
Copiar código

---

## 🏗️ Arquitectura (Clean Architecture)

```mermaid
flowchart TD
    A[Presentation Layer] --> B[Application Layer]
    B --> C[Domain Layer]
    C --> D[Infrastructure Layer]

    A:::layer
    B:::layer
    C:::layer
    D:::layer

    classDef layer fill:#f9f,stroke:#333,stroke-width:2px,color:#000;
🚀 Instalación y ejecución
🔹 Requisitos previos
Tener instalado Docker.

🔹 Pasos de instalación
Clonar el repositorio:

bash
Copiar código
git clone https://github.com/tuusuario/PropertyManager.git
Acceder a la carpeta raíz del proyecto:

bash
Copiar código
cd PropertyManager
Construir y levantar los contenedores:

bash
Copiar código
docker compose up -d --build
🧪 Ejemplo de uso rápido
🔹 Crear un propietario
bash
Copiar código
curl -X POST http://localhost:5000/api/owners \
  -H "Content-Type: application/json" \
  -d '{
        "name": "Juan Pérez",
        "address": "Calle Falsa 123",
        "email": "juan@example.com",
        "userName": "juanperez",
        "password": "SuperSecreta123"
      }'
🔹 Crear una propiedad
bash
Copiar código
curl -X POST http://localhost:5000/api/properties \
  -H "Content-Type: application/json" \
  -d '{
        "name": "Casa en la playa",
        "address": "Av. del Mar 456",
        "price": 250000,
        "year": 2021,
        "codeInternal": "CASA-001",
        "idOwner": 1
      }'
🔹 Listar propiedades con filtros
bash
Copiar código
curl "http://localhost:5000/api/properties?name=Casa&page=1&pageSize=10"
🛡️ Licencia
Este proyecto se distribuye bajo la licencia MIT.
Siéntete libre de usarlo, modificarlo y contribuir. 🙌

yaml
Copiar código

---

¿Quieres que también te prepare un **badge de build/status** (Docker, .NET, MIT License, etc.) para que tu README se vea aún más pro en GitHub?







Preguntar a ChatGPT
