# Manual de Usuario — Sistema de Gestión Mueblería
**Aplicación:** Mueblería Los Alpes  
**Versión:** 1.0  
**Fecha:** Mayo 2026  

---

## Tabla de Contenidos

1. [Introducción](#1-introducción)
2. [Requisitos del Sistema](#2-requisitos-del-sistema)
3. [Acceso al Sistema](#3-acceso-al-sistema)
4. [Interfaz General](#4-interfaz-general)
5. [Módulo: Página de Inicio](#5-módulo-página-de-inicio)
6. [Módulo: Gestión de Clientes](#6-módulo-gestión-de-clientes)
7. [Módulo: Acerca De](#7-módulo-acerca-de)
8. [Módulo: Contacto](#8-módulo-contacto)
9. [Diagnóstico de Conexión](#9-diagnóstico-de-conexión)
10. [Manejo de Errores](#10-manejo-de-errores)
11. [Preguntas Frecuentes](#11-preguntas-frecuentes)
12. [Soporte Técnico](#12-soporte-técnico)

---

## 1. Introducción

El **Sistema de Gestión Mueblería** es una aplicación web diseñada para administrar las operaciones de una mueblería. Permite gestionar clientes, consultar información del negocio y mantener el control de las operaciones comerciales.

### ¿Qué puede hacer con este sistema?

| Funcionalidad | Estado |
|---|---|
| Consultar lista de clientes | Disponible |
| Ver información de cada cliente | Disponible |
| Agregar nuevos clientes | Próximamente |
| Editar datos de clientes | Próximamente |
| Eliminar clientes | Próximamente |
| Gestión de productos y muebles | Próximamente |
| Carrito de compras | Próximamente |
| Órdenes y pedidos | Próximamente |
| Reportes de ventas | Próximamente |

---

## 2. Requisitos del Sistema

### Para el Administrador del Sistema

| Componente | Requisito |
|---|---|
| Sistema Operativo | Windows 10 / Windows 11 |
| Framework | .NET Framework 4.8.1 |
| Servidor Web | IIS o IIS Express |
| Base de Datos | Oracle Database (XE o superior) |
| Puerto de BD | 1522 |
| Nombre de BD | XEPDB1 |

### Para el Usuario Final

| Componente | Requisito |
|---|---|
| Navegador web | Chrome, Firefox, Edge o Safari (versiones recientes) |
| Resolución de pantalla | Mínimo 1024 x 768 px |
| Conexión a red | Acceso a la red local donde está alojado el sistema |
| JavaScript | Habilitado en el navegador |

---

## 3. Acceso al Sistema

### 3.1 Cómo ingresar

1. Abra su navegador web preferido.
2. En la barra de direcciones, escriba la URL proporcionada por el administrador del sistema.  
   Ejemplo: `http://localhost/Muebleria` o la dirección IP del servidor.
3. Presione **Enter**.
4. El sistema cargará automáticamente la página de inicio.

> **Nota:** En la versión actual el sistema no requiere inicio de sesión. La autenticación estará disponible en versiones futuras.

### 3.2 Primer acceso

Al ingresar por primera vez verá la pantalla de bienvenida del sistema. No se requiere ninguna configuración adicional por parte del usuario.

---

## 4. Interfaz General

### 4.1 Barra de Navegación

En la parte superior de todas las páginas encontrará la barra de navegación con los siguientes elementos:

```
[ Logo / Nombre del Sistema ]   [ Inicio ]  [ Clientes ]  [ Acerca de ]  [ Contacto ]
```

| Elemento | Función |
|---|---|
| **Logo / Nombre** | Haga clic para regresar a la página de inicio |
| **Inicio** | Navega a la página principal |
| **Clientes** | Abre el módulo de gestión de clientes |
| **Acerca de** | Muestra información del sistema |
| **Contacto** | Muestra la información de contacto |

> **En dispositivos móviles:** La barra de navegación se colapsa en un menú tipo hamburguesa (☰). Tóquelo para desplegar las opciones.

### 4.2 Área de Contenido

Debajo de la barra de navegación se encuentra el área principal donde se muestra el contenido de cada módulo.

### 4.3 Pie de Página

En la parte inferior de cada página se muestra información básica del sistema.

---

## 5. Módulo: Página de Inicio

**Cómo acceder:** Haga clic en **Inicio** en la barra de navegación, o en el logo del sistema.

La página de inicio presenta una bienvenida general al sistema y puede incluir accesos directos a las funcionalidades principales. Desde aquí puede navegar a cualquier módulo del sistema usando el menú superior.

---

## 6. Módulo: Gestión de Clientes

**Cómo acceder:** Haga clic en **Clientes** en la barra de navegación.

Este es el módulo principal del sistema. Permite visualizar y administrar la información de todos los clientes registrados en la mueblería.

### 6.1 Pantalla de Lista de Clientes

Al ingresar al módulo verá una pantalla con:

- Un **encabezado** que indica "Gestión de Clientes".
- El **total de clientes** registrados en el sistema.
- Un botón **"Nuevo Cliente"** (próximamente disponible).
- Las **tarjetas de clientes**, una por cada cliente registrado.

#### Información que muestra cada tarjeta de cliente

```
┌─────────────────────────────────────┐
│  [Ícono]  Nombre del Cliente        │
│  ─────────────────────────────────  │
│  📧  correo@ejemplo.com             │
│  📅  Fecha de registro: 01/01/2025  │
│       ID: [123]                     │
│  ─────────────────────────────────  │
│  [ Editar ]        [ Eliminar ]     │
└─────────────────────────────────────┘
```

| Campo | Descripción |
|---|---|
| **Nombre** | Nombre completo del cliente |
| **Correo** | Dirección de correo electrónico (haga clic para enviar un email) |
| **Fecha de registro** | Fecha y hora en que fue registrado el cliente (formato dd/mm/aaaa hh:mm) |
| **ID** | Identificador único del cliente en el sistema |

### 6.2 Acciones Disponibles

#### Ver correo electrónico del cliente
Haga clic sobre la dirección de correo que aparece en la tarjeta del cliente. Su cliente de correo predeterminado abrirá automáticamente un nuevo mensaje dirigido a esa dirección.

#### Agregar nuevo cliente *(Próximamente)*
El botón **"Nuevo Cliente"** ubicado en la parte superior derecha estará disponible en versiones futuras del sistema.

#### Editar cliente *(Próximamente)*
El botón **"Editar"** en cada tarjeta permitirá modificar los datos del cliente. Esta función estará disponible próximamente.

#### Eliminar cliente *(Próximamente)*
El botón **"Eliminar"** en cada tarjeta permitirá remover un cliente del sistema. Esta función estará disponible próximamente.

### 6.3 Pantalla Sin Clientes

Si aún no hay clientes registrados en el sistema, la pantalla mostrará un mensaje indicando que no existen clientes y el total aparecerá en cero (0).

### 6.4 Mensaje de Error en Clientes

Si ocurre un problema al cargar la lista de clientes (por ejemplo, un problema de conexión con la base de datos), el sistema mostrará un recuadro de alerta en color rojo con la descripción del error. En ese caso, consulte la sección [10. Manejo de Errores](#10-manejo-de-errores).

---

## 7. Módulo: Acerca De

**Cómo acceder:** Haga clic en **Acerca de** en la barra de navegación.

Esta sección muestra información general sobre el sistema: versión, descripción y datos relevantes del software. Es útil para conocer qué versión del sistema está en uso al contactar al soporte técnico.

---

## 8. Módulo: Contacto

**Cómo acceder:** Haga clic en **Contacto** en la barra de navegación.

Esta sección muestra la información de contacto de la mueblería:

| Tipo | Información |
|---|---|
| **Dirección física** | Dirección de las instalaciones de la mueblería |
| **Teléfono** | Números de contacto telefónico |
| **Correo de soporte** | Email para soporte técnico y consultas del sistema |
| **Correo de marketing** | Email para consultas comerciales |

---

## 9. Diagnóstico de Conexión

El sistema cuenta con una herramienta de diagnóstico para verificar que la conexión con la base de datos esté funcionando correctamente. Esta herramienta es utilizada principalmente por el administrador del sistema.

**Cómo acceder:** Navegue a la dirección `/TestDB/TestConexion` en su navegador.

El sistema responderá con un mensaje JSON indicando:

- **Conexión exitosa:** `{ "success": true, "message": "Conexión exitosa" }`
- **Error de conexión:** `{ "success": false, "message": "Descripción del error" }`

Esta herramienta es útil para diagnosticar problemas de base de datos sin necesidad de acceder al servidor directamente.

---

## 10. Manejo de Errores

### 10.1 Error al cargar clientes

**Síntoma:** Aparece un recuadro rojo con un mensaje de error en la pantalla de clientes.

**Posibles causas y soluciones:**

| Causa | Solución |
|---|---|
| La base de datos Oracle no está en funcionamiento | Contacte al administrador del sistema para que inicie el servicio de Oracle |
| La contraseña de la base de datos es incorrecta | El administrador debe verificar la configuración en `Web.config` |
| El servidor de base de datos no es accesible | Verifique que el servidor esté encendido y en red |
| Error en el procedimiento almacenado | El administrador debe revisar el paquete `PKG_CLIENTES` en Oracle |

### 10.2 Página de error general

Si el sistema encuentra un error inesperado, mostrará una página de error genérica. En ese caso:

1. Tome nota del mensaje de error que aparece en pantalla.
2. Registre la fecha y hora exacta en que ocurrió el problema.
3. Describa qué acción estaba realizando cuando ocurrió el error.
4. Comuníquese con el soporte técnico con esa información.

### 10.3 Página en blanco o el sistema no carga

| Síntoma | Acción recomendada |
|---|---|
| La página no carga | Verifique su conexión a la red local |
| Pantalla en blanco | Recargue la página con F5 |
| Error 404 (página no encontrada) | Verifique que la URL sea correcta |
| Error 500 (error del servidor) | Contacte al administrador del sistema |

---

## 11. Preguntas Frecuentes

**¿Por qué no puedo agregar nuevos clientes?**  
La función de creación, edición y eliminación de clientes está en desarrollo y estará disponible en una versión próxima del sistema. Por el momento, los registros deben ingresarse directamente en la base de datos Oracle.

**¿Por qué no aparecen clientes en la lista?**  
Puede deberse a dos razones: (1) aún no hay clientes registrados en la base de datos, o (2) hay un problema de conexión. Si hay datos en la base de datos pero no aparecen, contacte al administrador.

**¿El correo electrónico que aparece en las tarjetas es funcional?**  
Sí. Al hacer clic en la dirección de correo de un cliente, se abrirá su cliente de correo predeterminado con un nuevo mensaje dirigido a ese cliente.

**¿Cómo sé si el sistema está conectado a la base de datos?**  
El administrador puede verificar la conexión accediendo a `/TestDB/TestConexion` en el navegador. Si aparece `"success": true`, la conexión está activa.

**¿El sistema funciona en mi teléfono o tablet?**  
Sí. El sistema está diseñado con un diseño responsivo (Bootstrap 5) que se adapta a diferentes tamaños de pantalla, incluyendo móviles y tablets.

**¿Cuántos clientes puede manejar el sistema?**  
El sistema no tiene un límite predefinido por parte de la aplicación. La capacidad depende de la configuración de la base de datos Oracle.

---

## 12. Soporte Técnico

Para reportar problemas, solicitar nuevas funcionalidades o recibir asistencia técnica, contacte al equipo de desarrollo:

| Canal | Información |
|---|---|
| **Correo electrónico** | martir.dev@gmail.com |
| **Sistema de tickets** | Consulte con su administrador |

### Al contactar soporte, incluya:

1. Descripción detallada del problema.
2. Pasos exactos que realizó antes de que ocurriera el error.
3. Captura de pantalla del error (si aplica).
4. Nombre del navegador y versión que utiliza.
5. Fecha y hora en que ocurrió el problema.

---

*Manual elaborado para el Sistema de Gestión Mueblería — versión 1.0*  
*Última actualización: Mayo 2026*
