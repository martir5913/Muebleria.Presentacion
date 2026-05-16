# Plan de Administración de la Configuración e Integración
## Sistema Mueblería — Manual de Instalación y Configuración

**Proyecto:** Muebleria.Presentacion  
**Tecnología:** ASP.NET MVC 5 — Visual Basic .NET — Oracle Database  
**Versión del documento:** 1.0  
**Fecha:** Mayo 2026  
**Autor:** Jose Abraham Castillo

---

## Tabla de Contenidos

1. [Propósito y Alcance](#1-propósito-y-alcance)
2. [Arquitectura del Sistema](#2-arquitectura-del-sistema)
3. [Herramientas Requeridas](#3-herramientas-requeridas)
4. [Instalación del Entorno de Desarrollo](#4-instalación-del-entorno-de-desarrollo)
   - 4.1 [Visual Studio 2022](#41-visual-studio-2022)
   - 4.2 [.NET Framework 4.8](#42-net-framework-48)
   - 4.3 [Git](#43-git)
   - 4.4 [Oracle Database XE](#44-oracle-database-xe)
   - 4.5 [Oracle SQL Developer](#45-oracle-sql-developer)
5. [Obtención del Código Fuente](#5-obtención-del-código-fuente)
6. [Configuración de la Base de Datos](#6-configuración-de-la-base-de-datos)
7. [Configuración del Proyecto](#7-configuración-del-proyecto)
8. [Gestión de Dependencias NuGet](#8-gestión-de-dependencias-nuget)
9. [Verificación de la Instalación](#9-verificación-de-la-instalación)
10. [Plan de Administración de la Configuración](#10-plan-de-administración-de-la-configuración)
11. [Ambientes de Despliegue](#11-ambientes-de-despliegue)
12. [Solución de Problemas](#12-solución-de-problemas)

---

## 1. Propósito y Alcance

Este documento describe el **plan de administración de la configuración** y el **manual de instalación** de todas las herramientas, lenguajes y componentes necesarios para ejecutar, desarrollar y mantener el sistema Mueblería.

### Propósito

- Establecer los procedimientos para configurar el entorno de desarrollo desde cero.
- Documentar las versiones exactas de cada herramienta y componente usado.
- Definir las reglas para el control de versiones y gestión de cambios del proyecto.
- Servir como guía de referencia para nuevos integrantes del equipo.

### Alcance

Este plan aplica a todos los proyectos de la solución:

| Proyecto | Tipo | Descripción |
|----------|------|-------------|
| `Muebleria.Presentacion` | ASP.NET MVC | Capa de presentación (UI) |
| `Muebleria.Negocio` | Class Library | Lógica de negocio |
| `Muebleria.Datos` | Class Library | Acceso a datos Oracle |
| `Muebleria.Entidades` | Class Library | Modelos compartidos (POCOs) |

---

## 2. Arquitectura del Sistema

El sistema implementa una arquitectura **N-Capas** con separación estricta de responsabilidades:

```
┌──────────────────────────────────────────────────────────┐
│                   CAPA DE PRESENTACIÓN                    │
│              Muebleria.Presentacion                       │
│   Controllers / Views (.vbhtml) / App_Start / Helpers    │
│            ASP.NET MVC 5 — Visual Basic .NET             │
└─────────────────────────┬────────────────────────────────┘
                          │ referencias
┌─────────────────────────▼────────────────────────────────┐
│                   CAPA DE NEGOCIO                         │
│               Muebleria.Negocio                           │
│    CN_ProductosService / CN_CarritoService / ...          │
└─────────────────────────┬────────────────────────────────┘
                          │ referencias
┌─────────────────────────▼────────────────────────────────┐
│                 CAPA DE ACCESO A DATOS                    │
│                 Muebleria.Datos                           │
│    CD_Productos / CD_Clientes / CD_Usuarios / ...        │
│           Oracle.ManagedDataAccess 23.26.200             │
└─────────────────────────┬────────────────────────────────┘
                          │ referencias
┌─────────────────────────▼────────────────────────────────┐
│                 CAPA DE ENTIDADES                         │
│               Muebleria.Entidades                         │
│   CE_Producto / CE_Cliente / CE_Orden / CE_Usuario / ... │
└──────────────────────────────────────────────────────────┘
                          │
              ┌───────────▼───────────┐
              │   Oracle Database XE   │
              │  localhost:1522/xepdb1 │
              │    PKG_PRODUCTOS       │
              │    PKG_CLIENTES        │
              │    PKG_USUARIOS        │
              │    PKG_ORDENES         │
              └───────────────────────┘
```

---

## 3. Herramientas Requeridas

A continuación se listan **todas las herramientas, lenguajes y componentes** necesarios con sus versiones exactas:

### Herramientas de Desarrollo

| Herramienta | Versión | Rol en el Proyecto |
|-------------|---------|---------------------|
| Visual Studio | 2022 (v17.x) | IDE principal |
| .NET Framework | 4.8 | Runtime de la aplicación |
| Visual Basic .NET | 16.x (integrado en VS2022) | Lenguaje de programación |
| Git | 2.x | Control de versiones |

### Base de Datos

| Herramienta | Versión | Rol en el Proyecto |
|-------------|---------|---------------------|
| Oracle Database XE | 21c o superior | Motor de base de datos |
| Oracle SQL Developer | 23.x | Administración y scripting de BD |
| Oracle.ManagedDataAccess | 23.26.200 (NuGet) | Driver de conexión .NET–Oracle |

### Paquetes NuGet (dependencias del proyecto)

| Paquete | Versión | Rol |
|---------|---------|-----|
| `Microsoft.AspNet.Mvc` | 5.2.9 | Framework MVC |
| `Microsoft.AspNet.Razor` | 3.2.9 | Motor de vistas |
| `Microsoft.AspNet.WebApi` | 5.2.9 | Web API |
| `Microsoft.AspNet.WebPages` | 3.2.9 | Páginas web |
| `Microsoft.AspNet.Web.Optimization` | 1.1.3 | Bundling/minificación |
| `Oracle.ManagedDataAccess` | 23.26.200 | Conexión Oracle |
| `Newtonsoft.Json` | 13.0.3 | Serialización JSON |
| `bootstrap` | 5.2.3 | Framework CSS |
| `jQuery` | 3.7.0 | Librería JavaScript |
| `jQuery.Validation` | 1.19.5 | Validación cliente |
| `Microsoft.jQuery.Unobtrusive.Validation` | 3.2.11 | Validación no intrusiva |
| `Modernizr` | 2.8.3 | Compatibilidad de navegadores |
| `WebGrease` | 1.6.0 | Optimización de recursos |
| `Antlr` | 3.5.0.2 | Parser (dependencia interna) |
| `Microsoft.Web.Infrastructure` | 2.0.0 | Infraestructura web |
| `Microsoft.CodeDom.Providers.DotNetCompilerPlatform` | 4.1.0 | Compilador Roslyn |

---

## 4. Instalación del Entorno de Desarrollo

### 4.1 Visual Studio 2022

**Propósito:** IDE principal para desarrollar, compilar y depurar la solución.

#### Pasos de instalación

1. Descargar el instalador desde:  
   `https://visualstudio.microsoft.com/vs/`

2. Ejecutar `vs_setup.exe` y seleccionar la carga de trabajo:
   - **Desarrollo web y ASP.NET**
   - Asegurarse de incluir:
     - ASP.NET y desarrollo web
     - Herramientas de desarrollo de .NET Framework 4.8

3. En la pestaña **Componentes individuales**, verificar:
   - `.NET Framework 4.8 targeting pack`
   - `Visual Basic`
   - `Git para Windows` (opcional si ya tienes Git instalado)

4. Hacer clic en **Instalar** y esperar a que finalice.

#### Verificación

Abrir Visual Studio → Ayuda → Acerca de Microsoft Visual Studio  
Confirmar que la versión es **2022 (17.x o superior)**

---

### 4.2 .NET Framework 4.8

**Propósito:** Runtime sobre el que se ejecuta la aplicación web.

> En la mayoría de equipos con Windows 10/11 ya viene instalado. Si no está, se puede descargar por separado.

#### Pasos de instalación

1. Verificar si ya está instalado:
   ```powershell
   Get-ChildItem "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\" | Get-ItemProperty | Select-Object Release, Version
   ```
   Si el valor `Release` es **528040 o superior**, .NET 4.8 ya está instalado.

2. Si no está instalado, descargarlo desde:  
   `https://dotnet.microsoft.com/download/dotnet-framework/net48`

3. Ejecutar el instalador y seguir el asistente.

4. Reiniciar el equipo al finalizar.

#### Verificación

```powershell
[System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()
```
Debe mostrar una ruta que incluya `v4.0.xxxxx`.

---

### 4.3 Git

**Propósito:** Control de versiones del código fuente.

#### Pasos de instalación

1. Descargar desde: `https://git-scm.com/download/win`

2. Ejecutar el instalador con las siguientes opciones recomendadas:
   - Editor predeterminado: **Visual Studio Code** o el de tu preferencia
   - Ajuste del PATH: **Git from the command line and also from 3rd-party software**
   - Librería SSL: **Use the OpenSSL library**
   - Finales de línea: **Checkout Windows-style, commit Unix-style line endings**

3. Finalizar la instalación.

#### Configuración inicial obligatoria

Abrir una terminal (PowerShell o Git Bash) y ejecutar:

```bash
git config --global user.name "Tu Nombre"
git config --global user.email "tu.correo@ejemplo.com"
git config --global core.autocrlf true
```

#### Verificación

```bash
git --version
```
Debe mostrar `git version 2.x.x`.

---

### 4.4 Oracle Database XE

**Propósito:** Motor de base de datos relacional donde se almacena toda la información del sistema.

#### Pasos de instalación

1. Descargar Oracle Database 21c XE (Express Edition — gratuito) desde:  
   `https://www.oracle.com/database/technologies/xe-downloads.html`

2. Ejecutar `OracleXE213_Win64.zip` → extraer → ejecutar `setup.exe`.

3. Durante el asistente:
   - Aceptar la licencia
   - Definir la contraseña para los usuarios `SYS` y `SYSTEM`
   - Anotar el puerto asignado (por defecto **1521**, en algunos casos **1522**)

4. Al finalizar, Oracle creará automáticamente la PDB `XEPDB1`.

#### Verificar el servicio

```powershell
Get-Service -Name "OracleServiceXE" | Select-Object Status, Name
```
El estado debe ser `Running`.

#### Crear el usuario de la aplicación

Conectarse como `SYSTEM` usando SQL Developer o SQL*Plus:

```sql
-- Conectar al contenedor de la PDB
ALTER SESSION SET CONTAINER = XEPDB1;

-- Crear el usuario de la aplicación
CREATE USER muebleria_app IDENTIFIED BY Password123;

-- Otorgar permisos necesarios
GRANT CONNECT, RESOURCE TO muebleria_app;
GRANT CREATE SESSION TO muebleria_app;
GRANT CREATE TABLE TO muebleria_app;
GRANT CREATE PROCEDURE TO muebleria_app;
GRANT CREATE SEQUENCE TO muebleria_app;
GRANT UNLIMITED TABLESPACE TO muebleria_app;
```

---

### 4.5 Oracle SQL Developer

**Propósito:** Herramienta gráfica para administrar la base de datos, ejecutar scripts y depurar procedimientos PL/SQL.

#### Pasos de instalación

1. Descargar desde:  
   `https://www.oracle.com/tools/downloads/sqldev-downloads.html`  
   (Seleccionar la versión **"with JDK included"** para evitar instalar Java por separado)

2. Extraer el ZIP en una ubicación permanente (ej: `C:\OracleSQLDeveloper\`).

3. Ejecutar `sqldeveloper.exe`.

#### Configurar la conexión de desarrollo

En SQL Developer → Nueva Conexión:

| Campo | Valor |
|-------|-------|
| Nombre | `Muebleria-Local` |
| Usuario | `muebleria_app` |
| Contraseña | `Password123` |
| Rol | `default` |
| Tipo de conexión | `Basic` |
| Host | `localhost` |
| Puerto | `1522` (o el asignado durante la instalación) |
| Nombre de servicio | `xepdb1` |

Hacer clic en **Probar** para verificar, luego en **Guardar** y **Conectar**.

---

## 5. Obtención del Código Fuente

### Clonar el repositorio

```bash
git clone <url-del-repositorio> Muebleria.Presentacion
cd Muebleria.Presentacion
```

### Abrir la solución

1. En Visual Studio: **Archivo → Abrir → Proyecto o solución**
2. Navegar a la carpeta clonada y abrir `Muebleria.Presentacion.slnx`

### Estructura de proyectos en la solución

Al abrir la solución, el Explorador de soluciones mostrará 4 proyectos:

```
Muebleria.Presentacion (solución)
├── Muebleria.Entidades     ← sin dependencias externas
├── Muebleria.Datos         ← depende de Entidades
├── Muebleria.Negocio       ← depende de Datos y Entidades
└── Muebleria.Presentacion  ← depende de Negocio, Datos y Entidades
```

---

## 6. Configuración de la Base de Datos

Una vez conectado con el usuario `muebleria_app` en SQL Developer, ejecutar los scripts en el siguiente orden:

### 6.1 Crear tabla CLIENTES (script de ejemplo/prueba)

```sql
CREATE TABLE CLIENTES (
    ID             NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    NOMBRE         VARCHAR2(100),
    CORREO         VARCHAR2(150),
    FECHA_CREACION DATE DEFAULT SYSDATE
);
```

### 6.2 Insertar datos de prueba

```sql
INSERT INTO CLIENTES (NOMBRE, CORREO) VALUES ('Juan Perez', 'juan@test.com');
INSERT INTO CLIENTES (NOMBRE, CORREO) VALUES ('Maria Lopez', 'maria@test.com');
INSERT INTO CLIENTES (NOMBRE, CORREO) VALUES ('Carlos Ruiz', 'carlos@test.com');
COMMIT;
```

### 6.3 Crear paquete PKG_CLIENTES — Cabecera

```sql
CREATE OR REPLACE PACKAGE PKG_CLIENTES AS
    TYPE T_CURSOR IS REF CURSOR;
    PROCEDURE OBTENER_CLIENTES(p_cursor OUT T_CURSOR);
END PKG_CLIENTES;
```

### 6.4 Crear paquete PKG_CLIENTES — Cuerpo

```sql
CREATE OR REPLACE PACKAGE BODY PKG_CLIENTES AS
    PROCEDURE OBTENER_CLIENTES(p_cursor OUT T_CURSOR) IS
    BEGIN
        OPEN p_cursor FOR
            SELECT ID, NOMBRE, CORREO, FECHA_CREACION
            FROM CLIENTES
            ORDER BY ID;
    END OBTENER_CLIENTES;
END PKG_CLIENTES;
```

### 6.5 Verificar la creación

```sql
-- Verificar que la tabla existe
SELECT TABLE_NAME FROM USER_TABLES WHERE TABLE_NAME = 'CLIENTES';

-- Verificar que los paquetes existen
SELECT OBJECT_NAME, OBJECT_TYPE, STATUS
FROM USER_OBJECTS
WHERE OBJECT_TYPE IN ('PACKAGE', 'PACKAGE BODY')
ORDER BY OBJECT_TYPE, OBJECT_NAME;
```

> Para los scripts completos de todas las entidades del sistema, consultar:  
> `Muebleria.Presentacion/Documentacion/ScriptSQL.md`

---

## 7. Configuración del Proyecto

### 7.1 Cadena de conexión — Web.config

Abrir el archivo `Muebleria.Presentacion/Web.config` y localizar la sección `<connectionStrings>`:

```xml
<connectionStrings>
  <add name="OracleDb"
       connectionString="User Id=muebleria_app;Password=Password123;Data Source=localhost:1522/xepdb1;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

#### Parámetros a ajustar según entorno

| Parámetro | Descripción | Valor de referencia |
|-----------|-------------|---------------------|
| `User Id` | Usuario Oracle creado en el paso 4.4 | `muebleria_app` |
| `Password` | Contraseña del usuario Oracle | `Password123` |
| `localhost` | IP o nombre del servidor Oracle | `localhost` o IP remota |
| `1522` | Puerto del listener Oracle | `1521` o `1522` |
| `xepdb1` | Nombre del servicio (PDB) | `xepdb1`, `ORCL`, etc. |

#### Configuración para entorno local

```xml
<add name="OracleDb"
     connectionString="User Id=muebleria_app;Password=Password123;Data Source=localhost:1522/xepdb1;"
     providerName="Oracle.ManagedDataAccess.Client" />
```

#### Configuración para entorno remoto

```xml
<add name="OracleDb"
     connectionString="User Id=muebleria_app;Password=Password123;Data Source=192.168.1.100:1521/xepdb1;"
     providerName="Oracle.ManagedDataAccess.Client" />
```

### 7.2 Configuraciones adicionales en Web.config

El archivo `Web.config` también contiene las siguientes secciones que **no requieren modificación** en instalaciones estándar:

| Sección | Valor | Descripción |
|---------|-------|-------------|
| `targetFramework` | `4.8` | Versión de .NET del proyecto |
| `culture` | `es-GT` | Cultura regional (Guatemala) |
| `authentication mode` | `Forms` | Autenticación por formularios |
| `loginUrl` | `~/Cuenta/Login` | URL de inicio de sesión |
| `timeout` | `2880` | Sesión de 48 horas |

### 7.3 Configuración por ambiente

El proyecto incluye transformaciones de `Web.config` por ambiente:

| Archivo | Ambiente | Uso |
|---------|----------|-----|
| `Web.config` | Base | Configuración base del proyecto |
| `Web.Debug.config` | Desarrollo | Sobrescribe valores para Debug |
| `Web.Release.config` | Producción | Sobrescribe valores para Release |

Para modificar valores específicos de producción, editar `Web.Release.config`.

---

## 8. Gestión de Dependencias NuGet

### 8.1 Restaurar paquetes automáticamente

Al compilar por primera vez, Visual Studio restaura todos los paquetes listados en `packages.config` de forma automática.

Si la restauración falla, ejecutar manualmente en la **Consola de Administrador de paquetes** (Visual Studio → Herramientas → Administrador de paquetes NuGet):

```powershell
Update-Package -reinstall
```

O desde la terminal en la raíz de la solución:

```powershell
nuget restore Muebleria.Presentacion.slnx
```

### 8.2 Instalar un nuevo paquete

Para agregar una nueva dependencia al proyecto de presentación:

```powershell
Install-Package <NombreDelPaquete> -ProjectName Muebleria.Presentacion
```

### 8.3 Actualizar un paquete existente

```powershell
Update-Package <NombreDelPaquete> -ProjectName Muebleria.Presentacion
```

### 8.4 Ubicación de los paquetes

Todos los paquetes NuGet se almacenan localmente en:

```
Muebleria.Presentacion/
└── packages/               <- Carpeta de paquetes NuGet (excluida del repositorio)
    ├── bootstrap.5.2.3/
    ├── jQuery.3.7.0/
    ├── Oracle.ManagedDataAccess.23.26.200/
    └── ...
```

> La carpeta `packages/` está incluida en `.gitignore` y no se sube al repositorio. Siempre se regenera al restaurar NuGet.

---

## 9. Verificación de la Instalación

Completar el siguiente checklist en orden para confirmar que el entorno está correctamente configurado:

### Checklist de Instalación

- [ ] **Visual Studio 2022** instalado con carga de trabajo ASP.NET y desarrollo web
- [ ] **.NET Framework 4.8** disponible (verificado con PowerShell)
- [ ] **Git** instalado y configurado con nombre y correo de usuario
- [ ] **Oracle Database XE** en ejecución (servicio `OracleServiceXE` activo)
- [ ] **Usuario `muebleria_app`** creado con los permisos necesarios
- [ ] **Oracle SQL Developer** conectado correctamente a `xepdb1`
- [ ] **Scripts SQL** ejecutados (tablas y paquetes creados)
- [ ] **Repositorio clonado** y solución abierta en Visual Studio
- [ ] **Web.config** actualizado con la cadena de conexión correcta
- [ ] **Paquetes NuGet** restaurados sin errores
- [ ] **Compilación exitosa** de toda la solución (Ctrl + Shift + B)
- [ ] **Test de conexión** exitoso en `http://localhost:PUERTO/TestDB`
- [ ] **Aplicación ejecutándose** y catálogo visible en `http://localhost:PUERTO/Catalogo`

### Prueba de conexión a la base de datos

Iniciar la aplicación (F5) y navegar a:

```
http://localhost:PUERTO/TestDB/TestConexion
```

**Respuesta esperada:**
```json
{
  "success": true,
  "message": "Conexión exitosa a Oracle"
}
```

---

## 10. Plan de Administración de la Configuración

### 10.1 Control de versiones — Git

El proyecto usa **Git** como sistema de control de versiones. El repositorio sigue la siguiente estrategia de ramas:

#### Estructura de ramas

| Rama | Propósito | Política |
|------|-----------|----------|
| `master` | Rama principal — código estable | Solo recibe merges de ramas de feature/fix |
| `Terminado` | Rama de trabajo actual | Desarrollo activo del sprint |
| `feature/<nombre>` | Nueva funcionalidad | Creada desde `master`, merge al terminar |
| `fix/<nombre>` | Corrección de errores | Creada desde `master`, merge al terminar |

#### Crear una rama de funcionalidad

```bash
git checkout master
git pull origin master
git checkout -b feature/nombre-de-la-funcionalidad
```

#### Convenciones de commits

Los mensajes de commit deben seguir el formato:

```
<tipo>: <descripción corta en presente>

Tipos permitidos:
  feat:     nueva funcionalidad
  fix:      corrección de error
  refactor: reestructuración sin cambio de comportamiento
  docs:     cambios en documentación
  config:   cambios en archivos de configuración
  style:    cambios de formato o presentación
```

**Ejemplos:**
```
feat: agregar módulo de ofertas con descuentos por categoría
fix: corregir validación de stock negativo en carrito
refactor: modernizar CD_Usuarios con mejor manejo de recursos
docs: actualizar manual de instalación con pasos de Oracle XE
```

#### Flujo de trabajo recomendado

```bash
# 1. Actualizar la rama principal
git checkout master
git pull origin master

# 2. Crear rama de trabajo
git checkout -b feature/mi-funcionalidad

# 3. Desarrollar y hacer commits frecuentes
git add <archivos-específicos>
git commit -m "feat: descripción del cambio"

# 4. Mantener la rama actualizada con master
git fetch origin
git rebase origin/master

# 5. Integrar al terminar
git checkout master
git merge feature/mi-funcionalidad
git push origin master
```

### 10.2 Archivos excluidos del repositorio

El archivo `.gitignore` excluye automáticamente los siguientes elementos:

| Elemento excluido | Razón |
|-------------------|-------|
| `packages/` | Los paquetes NuGet se restauran automáticamente |
| `bin/` y `obj/` | Archivos compilados, generados por el build |
| `.vs/` | Configuración local de Visual Studio |
| `*.user` | Preferencias de usuario (paths locales) |
| `Web.Debug.config` | Configuraciones de ambiente local |

> **Nunca subir al repositorio:** cadenas de conexión con credenciales reales, archivos `.env` o configuraciones de producción.

### 10.3 Gestión de versiones de dependencias

Las versiones de todas las dependencias NuGet están fijadas en `packages.config`. **No actualizar paquetes sin pruebas previas**, especialmente:

- `Oracle.ManagedDataAccess` — cambios de versión pueden afectar la compatibilidad con la BD
- `Microsoft.AspNet.Mvc` — actualizaciones mayores requieren cambios en el código

Para actualizar una dependencia, seguir el proceso:

1. Crear una rama `fix/actualizar-<paquete>`
2. Actualizar el paquete y compilar
3. Ejecutar pruebas funcionales completas
4. Integrar a `master` solo si todo funciona

### 10.4 Identificación de versiones de componentes

Para consultar las versiones activas en cualquier momento:

```powershell
# Ver paquetes NuGet instalados
Get-Content .\Muebleria.Presentacion\packages.config

# Ver versión de .NET Framework instalada
(Get-ItemProperty "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\").Version

# Ver versión de Git
git --version

# Ver versión del driver Oracle (desde SQL Developer)
# Herramientas → Preferencias → Base de Datos → Información del controlador
```

---

## 11. Ambientes de Despliegue

### Ambiente de Desarrollo (Local)

| Componente | Configuración |
|------------|---------------|
| URL base | `http://localhost:PUERTO/` |
| Oracle Host | `localhost` |
| Oracle Puerto | `1522` |
| Oracle PDB | `xepdb1` |
| Usuario Oracle | `muebleria_app` |
| Modo compilación | `Debug` |
| Logs | Activados con detalle completo |

### Ambiente de Producción

Al publicar en producción, editar `Web.Release.config` con los valores del servidor de producción:

```xml
<connectionStrings>
  <add name="OracleDb"
       connectionString="User Id=muebleria_prod;Password=PROD_PASSWORD;Data Source=servidor-oracle:1521/PROD_SID;"
       providerName="Oracle.ManagedDataAccess.Client"
       xdt:Transform="SetAttributes"
       xdt:Locator="Match(name)" />
</connectionStrings>
```

#### Publicar desde Visual Studio

1. Clic derecho en `Muebleria.Presentacion` → **Publicar**
2. Seleccionar el perfil de publicación (IIS, carpeta, etc.)
3. Verificar que la configuración es **Release**
4. Hacer clic en **Publicar**

---

## 12. Solución de Problemas

### Tabla de errores comunes

| Error | Causa probable | Solución |
|-------|----------------|----------|
| `Unable to load Oracle.ManagedDataAccess` | Paquete NuGet no restaurado o faltante | Ejecutar `Update-Package -reinstall` en la Consola NuGet |
| `ORA-12514: TNS listener does not know of service` | Nombre del servicio incorrecto en `Data Source` | Verificar el nombre de la PDB en Oracle y actualizar `Web.config` |
| `ORA-01017: invalid username/password` | Credenciales incorrectas | Revisar `User Id` y `Password` en la cadena de conexión |
| `ORA-01045: user lacks CREATE SESSION privilege` | El usuario no tiene permisos | Ejecutar `GRANT CONNECT TO muebleria_app;` como SYSTEM |
| `Connection Timeout` | Oracle no está corriendo o el puerto es incorrecto | Verificar que el servicio `OracleServiceXE` está activo; revisar el puerto |
| Error de compilación en referencias | Proyectos no compilados en orden | Limpiar y recompilar la solución completa (Build → Clean Solution → Rebuild) |
| `The type or namespace name 'CE_Producto' could not be found` | Referencia entre proyectos no configurada | Verificar referencias del proyecto en Visual Studio → propiedades del proyecto |
| Página en blanco al ejecutar | Error de configuración de rutas | Revisar `App_Start/RouteConfig.vb` y la URL de inicio |
| `HTTP Error 403.14 - Forbidden` | IIS Express no encuentra el controlador home | Limpiar caché de Visual Studio y recompilar |

### Reiniciar el entorno desde cero

Si el entorno está en un estado inconsistente:

```bash
# 1. Limpiar archivos compilados
git clean -fdx bin/ obj/

# 2. En Visual Studio: Build → Clean Solution

# 3. Restaurar paquetes NuGet
nuget restore Muebleria.Presentacion.slnx

# 4. Recompilar
# En Visual Studio: Build → Rebuild Solution (Ctrl+Alt+F7)
```

---

## Referencias

| Recurso | URL / Ubicación |
|---------|-----------------|
| Documentación ASP.NET MVC 5 | `https://docs.microsoft.com/aspnet/mvc` |
| Oracle Data Provider for .NET | `https://docs.oracle.com/en/database/oracle/oracle-database/21/odpnt/` |
| Bootstrap 5.2 | `https://getbootstrap.com/docs/5.2/` |
| jQuery 3.7 | `https://api.jquery.com/` |
| Manual de conexión Oracle (proyecto) | `Documentacion/Manual.md` |
| Scripts de base de datos (proyecto) | `Documentacion/ScriptSQL.md` |
| Guía general del proyecto | `README.md` (raíz del repositorio) |

---

*Documento generado para el proyecto Muebleria.Presentacion — Mayo 2026*
