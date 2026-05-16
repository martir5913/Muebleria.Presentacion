# Muebleria.Presentacion

Sistema de e-commerce para una tienda de muebles, desarrollado en **ASP.NET MVC 5** con **Visual Basic .NET** y base de datos **Oracle**.

---

## Tabla de Contenidos

1. [Descripcion General](#descripcion-general)
2. [Arquitectura](#arquitectura)
3. [Requisitos Previos](#requisitos-previos)
4. [Configuracion](#configuracion)
5. [Modulos del Sistema](#modulos-del-sistema)
6. [Flujos Principales](#flujos-principales)
7. [Estructura del Proyecto](#estructura-del-proyecto)
8. [Base de Datos](#base-de-datos)
9. [Tecnologias](#tecnologias)
10. [Solucion de Problemas](#solucion-de-problemas)

---

## Descripcion General

**Muebleria.Presentacion** es una aplicacion web de e-commerce completa para la gestion y venta de muebles. Permite a los clientes navegar el catalogo, agregar productos al carrito y realizar compras, mientras que los administradores pueden gestionar productos, inventario, bodegas y generar reportes.

### Funcionalidades Principales

- Catalogo de productos con categorias y filtros
- Carrito de compras y proceso de checkout
- Registro e inicio de sesion de usuarios
- Panel de administracion (productos, bodegas, clientes)
- Gestion de inventario por bodega
- Generacion de reportes de ventas
- Gestion de ofertas y sugerencias de clientes

---

## Arquitectura

El sistema sigue una arquitectura **N-Capas (4 capas)** con separacion clara de responsabilidades:

```
┌─────────────────────────────────────────┐
│        Muebleria.Presentacion           │
│   (ASP.NET MVC - Controladores/Vistas)  │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│           Muebleria.Negocio             │
│        (Logica de Negocio - Services)   │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│            Muebleria.Datos              │
│       (Acceso a Datos - Oracle)         │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│          Muebleria.Entidades            │
│          (Modelos / POCOs)              │
└─────────────────────────────────────────┘
```

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| Presentacion | `Muebleria.Presentacion` | Controladores MVC, Vistas Razor, Configuracion de rutas |
| Negocio | `Muebleria.Negocio` | Servicios con logica de negocio y validaciones |
| Datos | `Muebleria.Datos` | Acceso a Oracle mediante paquetes PL/SQL |
| Entidades | `Muebleria.Entidades` | Clases POCO compartidas entre capas |

---

## Requisitos Previos

- **Visual Studio 2022** (con soporte para VB.NET y ASP.NET MVC)
- **.NET Framework 4.8**
- **Oracle Database** (XE, Standard o Enterprise)
  - Usuario: `muebleria_app`
  - Puerto por defecto: `1521` o `1522`
  - PDB/SID: `xepdb1` o segun configuracion local
- **NuGet** (para restaurar paquetes automaticamente)

---

## Configuracion

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd Muebleria.Presentacion
```

### 2. Restaurar paquetes NuGet

Abre la solucion `Muebleria.Presentacion.slnx` en Visual Studio. Los paquetes se restauran automaticamente al compilar.

### 3. Configurar la cadena de conexion

Edita el archivo `Muebleria.Presentacion/Web.config`:

```xml
<connectionStrings>
  <add name="OracleDb"
       connectionString="User Id=muebleria_app;Password=Password123;Data Source=localhost:1522/xepdb1;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

Ajusta los valores segun tu entorno:

| Parametro | Descripcion | Ejemplo |
|-----------|-------------|---------|
| `User Id` | Usuario Oracle | `muebleria_app` |
| `Password` | Contrasena | `Password123` |
| `Data Source` | `host:puerto/SID` | `localhost:1522/xepdb1` |

### 4. Verificar la conexion

Inicia la aplicacion y navega a:

```
http://localhost:PUERTO/TestDB
```

Haz clic en **"Probar Conexion"** para confirmar la conectividad con Oracle.

### 5. Compilar y ejecutar

Presiona `F5` en Visual Studio o usa:

```bash
msbuild Muebleria.Presentacion.slnx /p:Configuration=Debug
```

---

## Modulos del Sistema

### Catalogo (`/Catalogo`)

Permite a los visitantes navegar y buscar productos.

- Listado de productos con imagen, nombre, precio y descripcion
- Filtrado por categoria (Sala, Comedor, Habitaciones, Jardin, Oficina, Iluminacion)
- Boton para agregar al carrito directamente desde el catalogo

### Carrito (`/Carrito`)

Gestion de la sesion de compra del cliente.

- Ver productos agregados con cantidades y subtotales
- Eliminar items individuales o vaciar el carrito completo
- Procesar la compra y obtener confirmacion con numero de referencia

### Cuenta (`/Cuenta`)

Autenticacion y registro de usuarios.

- Inicio de sesion con usuario y contrasena
- Registro de nuevos clientes
- Roles disponibles: `ADMIN`, `CLIENTE`

### Productos (`/Productos`) — Solo Admin

Gestion del catalogo de productos (CRUD completo).

- Listar todos los productos con estado de inventario
- Crear nuevos productos con dimensiones, material, color y foto
- Editar informacion y precio/stock existente
- Eliminar productos del catalogo

### Bodegas (`/Bodegas`) — Solo Admin

Gestion de almacenes e inventario.

- Listar bodegas disponibles
- Ver stock de productos por bodega
- Crear y editar bodegas

### Clientes (`/Clientes`) — Solo Admin

Administracion de clientes registrados.

- Listado completo de clientes
- Ver perfil detallado de cada cliente
- Gestionar sugerencias recibidas

### Reportes (`/Reportes`) — Solo Admin

Generacion de informes del negocio.

- Reportes de ventas por periodo
- Analisis de productos mas vendidos

### Ofertas (`/Ofertas`) — Solo Admin

Configuracion de descuentos y promociones en productos.

---

## Flujos Principales

### Flujo de Compra (Cliente)

```
Login (/Cuenta/Login)
    ↓
Navegar Catalogo (/Catalogo)
    ↓
Agregar al Carrito (POST /Catalogo/AgregarAlCarrito)
    ↓
Ver Carrito (/Carrito)
    ↓
Confirmar Compra (POST /Carrito/Comprar)
    ↓
Confirmacion con # de referencia (/Carrito/Confirmacion)
```

### Flujo de Administracion

```
Login como ADMIN
    ↓
Gestionar Productos (/Productos)
    ↓
Crear / Editar / Eliminar
    ↓
Actualizar Inventario en Bodegas (/Bodegas/Stock)
    ↓
Revisar Reportes (/Reportes)
```

---

## Estructura del Proyecto

```
Muebleria.Presentacion/              <- Solucion raiz
├── Muebleria.Presentacion/          <- Capa de Presentacion
│   ├── App_Start/
│   │   ├── BundleConfig.vb          <- CSS/JS minificados
│   │   ├── FilterConfig.vb          <- Filtros globales MVC
│   │   └── RouteConfig.vb           <- Definicion de rutas
│   ├── Controllers/
│   │   ├── HomeController.vb
│   │   ├── CatalogoController.vb
│   │   ├── CarritoController.vb
│   │   ├── CuentaController.vb
│   │   ├── ProductosController.vb
│   │   ├── ClientesController.vb
│   │   ├── BodegasController.vb
│   │   ├── ReportesController.vb
│   │   ├── OfertasController.vb
│   │   └── TestDBController.vb
│   ├── Views/
│   │   ├── Shared/_Layout.vbhtml    <- Plantilla maestra
│   │   ├── Home/
│   │   ├── Cuenta/                  <- Login, Registro
│   │   ├── Catalogo/
│   │   ├── Carrito/                 <- Index, Confirmacion
│   │   ├── Productos/               <- Index, Crear, Editar
│   │   ├── Bodegas/                 <- Index, Stock, Crear, Editar
│   │   ├── Clientes/                <- Index, Perfil, Sugerencias
│   │   ├── Reportes/
│   │   └── Ofertas/
│   ├── Helpers/
│   │   ├── MockProductos.vb         <- 25 productos de demo
│   │   └── CategoryIcons.vb
│   ├── Content/                     <- Bootstrap 5.2.3 + CSS
│   ├── Scripts/                     <- jQuery 3.7.0 + Bootstrap JS
│   ├── Documentacion/
│   │   ├── Manual.md                <- Guia de configuracion detallada
│   │   └── ScriptSQL.md             <- Scripts de base de datos
│   └── Web.config                   <- Configuracion de la aplicacion
│
├── Muebleria.Negocio/               <- Capa de Logica de Negocio
│   ├── CN_ProductosService.vb
│   ├── CN_CarritoService.vb
│   ├── CN_OrdenesService.vb
│   ├── CN_UsuariosService.vb
│   ├── CN_ClientesService.vb
│   ├── CN_BodegasService.vb
│   ├── CN_ReportesService.vb
│   └── CN_ConexionService.vb
│
├── Muebleria.Datos/                 <- Capa de Acceso a Datos
│   ├── Conexion/CD_Conexion.vb
│   ├── Productos/CD_Productos.vb
│   ├── Carrito/CD_Carrito.vb
│   ├── Ordenes/CD_Ordenes.vb
│   ├── Usuarios/CD_Usuarios.vb
│   ├── Clientes/CD_Clientes.vb
│   ├── Bodegas/CD_Bodegas.vb
│   ├── Reportes/CD_Reportes.vb
│   └── Sugerencias/CD_Sugerencias.vb
│
└── Muebleria.Entidades/             <- Modelos compartidos
    ├── CE_Producto.vb
    ├── CE_Carrito.vb
    ├── CE_CarritoItem.vb
    ├── CE_Usuario.vb
    ├── CE_Cliente.vb
    ├── CE_Orden.vb
    ├── CE_OrdenDetalle.vb
    ├── CE_Bodega.vb
    ├── CE_Empleado.vb
    ├── CE_Reporte.vb
    ├── CE_Sugerencia.vb
    └── CE_Pago.vb
```

---

## Base de Datos

La aplicacion utiliza **Oracle Database** con paquetes PL/SQL.

### Paquetes Oracle consumidos

| Paquete | Descripcion |
|---------|-------------|
| `PKG_PRODUCTOS` | CRUD de productos, busqueda, inventario |
| `PKG_CLIENTES` | Registro y consulta de clientes |
| `PKG_USUARIOS` | Autenticacion y gestion de usuarios |
| `PKG_ORDENES` | Creacion y consulta de ordenes |
| `PKG_INVENTARIO` | Gestion de stock por bodega |

### Entidades principales

| Entidad | Descripcion |
|---------|-------------|
| `PRODUCTO` | Catalogo de muebles con precio, stock y dimensiones |
| `CLIENTE` | Datos personales y de contacto del comprador |
| `USUARIO` | Credenciales de acceso al sistema |
| `CARRITO` | Sesion de compra activa por cliente |
| `ORDEN` | Compra finalizada con detalles y forma de pago |
| `BODEGA` | Almacen fisico con inventario de productos |

> Los scripts de creacion de tablas y paquetes estan en:
> `Muebleria.Presentacion/Documentacion/ScriptSQL.md`

---

## Tecnologias

| Componente | Tecnologia / Version |
|------------|----------------------|
| Framework web | ASP.NET MVC 5.2.9 |
| Lenguaje | Visual Basic .NET |
| Runtime | .NET Framework 4.8 |
| Base de datos | Oracle Database |
| Driver Oracle | Oracle.ManagedDataAccess 23.26.200 |
| UI / CSS | Bootstrap 5.2.3 |
| JavaScript | jQuery 3.7.0 |
| Validacion | jQuery Validation 1.19.5 |
| JSON | Newtonsoft.Json 13.0.3 |

---

## Solucion de Problemas

| Error | Causa | Solucion |
|-------|-------|----------|
| `Unable to load Oracle.ManagedDataAccess` | Paquete NuGet no instalado | Restaurar NuGet o instalar manualmente `Oracle.ManagedDataAccess` |
| `ORA-12514 TNS:listener does not know of service` | SID/PDB incorrecto | Verificar el nombre del servicio en la cadena de conexion |
| `ORA-01017 invalid username/password` | Credenciales incorrectas | Revisar `User Id` y `Password` en `Web.config` |
| `Connection Timeout` | Servidor Oracle inaccesible | Verificar IP, puerto y que el servicio Oracle este activo |
| Error de compilacion en referencias | Proyectos no referenciados | Verificar referencias entre proyectos en Visual Studio |

Para una guia de configuracion mas detallada, consulta:
`Muebleria.Presentacion/Documentacion/Manual.md`

---

## Contacto

- **Desarrollador:** Jose Abraham Castillo
- **Email:** omarclaudeia@gmail.com
