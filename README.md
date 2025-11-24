# DockerVenta

## Descripción
DockerVenta es un microservicio desarrollado en **.NET 8** que permite gestionar el registro de ventas para una tienda pequeña de repuestos para autos. Este microservicio centraliza los procesos necesarios para manejar **clientes, productos y ventas**, incluyendo el detalle de los productos vendidos en cada venta.  

El proyecto está diseñado para ejecutarse dentro de **Docker**, utilizando SQL Server como base de datos, lo que facilita su despliegue y portabilidad.

---

## Funcionalidades principales
- **Clientes:** CRUD de clientes (crear, listar, actualizar, eliminar).  
- **Productos:** CRUD de productos, control de stock.  
- **Ventas:** Registrar ventas asociadas a un cliente, con uno o varios productos y cálculo automático del total y subtotal por item.  
- **Detalle de ventas:** Permite mantener el historial de productos vendidos por cada venta.

---
## Estructura del proyecto

DockerVenta/
│── Controllers/
│ ├── ClienteController.cs
│ ├── ProductoController.cs
│ └── VentaController.cs
│
│── Data/
│ └── VentaDb.cs
│
│── Models/
│ ├── Cliente.cs
│ ├── Producto.cs
│ ├── Venta.cs
│ └── VentaItem.cs
│
│── Dockerfile
│── docker-compose.yml
│── Program.cs
│── appsettings.json
│── DockerVenta.csproj
│── README.md
---

## Tecnologías
- **.NET 8**  
- **C#**  
- **Entity Framework Core**  
- **SQL Server 2022**  
- **Docker & Docker Compose**  
---

## Configuración y ejecución

1. Clonar el repositorio:

```bash
git clone <URL_REPOSITORIO>
cd DockerVenta

Construir y levantar los contenedores Docker:
docker compose up --build -d

Esto levantará dos contenedores:

SQL Server → Base de datos ProductDB.

DockerVenta → Microservicio corriendo en http://localhost:8002.

Acceder a los endpoints vía Postman o cualquier cliente HTTP.

Endpoints principales

Clientes
GET /api/cliente/listar → Listar todos los clientes
POST /api/cliente → Crear un cliente
PUT /api/cliente/{id} → Actualizar cliente
DELETE /api/cliente/{id} → Eliminar cliente

Productos
GET /api/producto/listar → Listar todos los productos
POST /api/producto → Crear un producto
PUT /api/producto/{id} → Actualizar producto
DELETE /api/producto/{id} → Eliminar producto

Ventas
GET /api/venta/listar → Listar todas las ventas con detalle
POST /api/venta → Registrar una venta con uno o varios productos

Notas
La relación entre Venta, VentaItem, Producto y Cliente está implementada para evitar ciclos de referencia en JSON.
La propiedad Stock de los productos se actualiza automáticamente al registrar una venta.
El microservicio está pensado como un único servicio que maneja todo el proceso de ventas de manera independiente.